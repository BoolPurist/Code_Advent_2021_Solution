using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent.PacketDecoder
{
  public enum SubPackageMode { None, LengthInBits, NumberOfPackages }

  public class Packet
  {
    public static string GetBinaryFromHex(string hex)
    {
      IEnumerable<string> binaryNumbers = hex.Select(hexSymbol => Convert.ToInt32(hexSymbol.ToString(), 16))
      .Select(decimalNumber => Convert.ToString(decimalNumber, 2).PadLeft(4, '0'));     
      return String.Join(String.Empty, binaryNumbers);
    }

    public int Version { get; private set; }

    public int ID { get; private set; }

    private int packetLength = 0;

    public int PacketLength
    {
      get
      {
        int total = packetLength;

        total += _subPackets.Aggregate(0, (sum, packet) => sum + packet.PacketLength);

        return total;
      }

    }

    public bool IsDirectLiteralValue { get; private set; }

    public ulong LiteralValue { get; private set; }

    public ulong ActualValue
    {
      get
      {
        
        return ID switch
        {
          0 => _subPackets.Aggregate(0UL, (sum, packet) => sum + packet.ActualValue),
          1 => GetProduct(),
          2 => _subPackets.Min(packet => packet.ActualValue),
          3 => _subPackets.Max(packet => packet.ActualValue),
          4 => LiteralValue,
          5 => _subPackets[0].ActualValue > _subPackets[1].ActualValue ? 1UL : 0UL,
          6 => _subPackets[0].ActualValue < _subPackets[1].ActualValue ? 1UL : 0UL,
          7 => _subPackets[0].ActualValue == _subPackets[1].ActualValue ? 1UL : 0UL,
          _ => throw new ArgumentOutOfRangeException(nameof(ID), $"Not expected direction value: {ID}")
        };

        ulong GetProduct()
        {
          if (_subPackets.Count == 0)
          {
            return 0UL;
          }
          else
          {
            return _subPackets.Aggregate(1UL, (prod, packet) => prod * packet.ActualValue);
          }
        }
      }
      
    }

    public SubPackageMode SubMode { get; private set; } = SubPackageMode.None;

    public int NumberOfSubPackets => _subPackets.Count;

    public int SubPackageDelimiter { get; private set; }

    private List<Packet> _subPackets = new();

    public Packet(string binaryLine)
    {
      ParseForVersion();
      ParseForId();
      packetLength = 6;
     
      if (IsDirectLiteralValue)
      {
        Debug.Assert(ID == 4);
        LiteralValue = ParseForLiteralValue(6, binaryLine);
      }
      else
      {
        SetSubPacketMode();
        long subDelimeter = GetSubPacketNumberOfLength();
        GetSubPackages(subDelimeter);        
      }

      void ParseForVersion()
        => Version = Convert.ToInt32(binaryLine.Substring(0, 3), 2);

      void ParseForId()
      {
        ID = Convert.ToInt32(binaryLine.Substring(3, 3), 2);
        IsDirectLiteralValue = ID == 4;
      }

      void SetSubPacketMode()
      {
        char digit = binaryLine[6];
        packetLength++;
        SubMode = digit == '0' ? SubPackageMode.LengthInBits : SubPackageMode.NumberOfPackages;
      }

      long GetSubPacketNumberOfLength()
      {
        int lengthOfDelimiter = SubMode == SubPackageMode.LengthInBits ?
          15 : 11;
        SubPackageDelimiter = lengthOfDelimiter;
        long result = SubMode == SubPackageMode.LengthInBits ?
        Convert.ToInt64(binaryLine.Substring(7, lengthOfDelimiter), 2)
        : Convert.ToInt64(binaryLine.Substring(7, lengthOfDelimiter), 2);

        packetLength += lengthOfDelimiter;
        return result;
      }

      void GetSubPackages(in long subDelimeter)
      {
        int currentIndexInBinary = 7 + SubPackageDelimiter;
        if (SubMode == SubPackageMode.LengthInBits)
        {
          int packetLength = 0;
          
          do
          {
            string subSequence = binaryLine.Substring(currentIndexInBinary);
            var nextPacket = new Packet(subSequence);
            packetLength += nextPacket.PacketLength;
            currentIndexInBinary += nextPacket.PacketLength;
            _subPackets.Add(nextPacket);
          } while (packetLength < subDelimeter);
          
        }
        else
        {
          for (int packetIndex = 0; packetIndex < subDelimeter; packetIndex++)
          {
            string subSequence = binaryLine.Substring(currentIndexInBinary);
            var nextPacket = new Packet(subSequence);
            currentIndexInBinary += nextPacket.PacketLength;
            _subPackets.Add(nextPacket);
          }
        }


      }
    }

    private ulong ParseForLiteralValue(int startBit, in string binaryLine)
    {
      var binaryParts = new Stack<string>();

      char leadingBit;

      do
      {
        string fiveDigits = binaryLine.Substring(startBit, 5);
        leadingBit = fiveDigits[0];
        startBit += 5;
        binaryParts.Push(fiveDigits.Substring(1));
      } while (leadingBit == '1');

      AdjustPacketLength();

      return GetNumberFromLiteralString();

      void AdjustPacketLength() => packetLength += binaryParts.Count * 5;

      ulong GetNumberFromLiteralString()
      {
        var factor = 1UL;
        var result = 0UL;

        do
        {
          string currentPart = binaryParts.Pop();

          for (int i = 3; i != -1; i--)
          {
            ulong currentDigit = (ulong)(currentPart[i] - '0');
            result += currentDigit * factor;
            factor *= 2;
          }

        } while (binaryParts.Count > 0);

        return result;
      }
    }

    public int GetSumOfVersion()
      => Version + _subPackets.Aggregate(0, (totalVersion, packet) => totalVersion + packet.GetSumOfVersion());
  }
}
