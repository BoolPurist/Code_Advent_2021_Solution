using NUnit.Framework;
using CodeOfAdvent.PacketDecoder;

namespace UnitTests
{
  public class Test_Packet
  {


    [Test]
    public void Test_DecodeWithId4()
    {
      // Set up & act
      var packet = new Packet("110100101111111000101000");

      // Assert
      AssertFor_Id_Version_PacketLength_LiteralValue_LiteralBool(
        packetToTestOn: packet,
        expectedVersion: 6,
        expectedId: 4,
        expectedLiteralBool: true,
        expectedPacketLength: 21,
        expectedLiteralValue: 2021
        );
    }

    private void AssertFor_Id_Version_PacketLength_LiteralValue_LiteralBool(
      Packet packetToTestOn,
      int expectedVersion,
      int expectedId,
      bool expectedLiteralBool,
      int expectedPacketLength,
      int expectedLiteralValue
      )
    {
      Assert.AreEqual(expectedVersion, packetToTestOn.Version);
      Assert.AreEqual(expectedId, packetToTestOn.ID);
      Assert.AreEqual(expectedLiteralBool, packetToTestOn.IsDirectLiteralValue);
      Assert.AreEqual(expectedPacketLength, packetToTestOn.PacketLength);
      Assert.AreEqual(expectedLiteralValue, packetToTestOn.LiteralValue);
    }

    private void Assert_NumberOfSubPacket_PacketLength(
      Packet packetToTestOn,
      in SubPackageMode expectedPackageMode,
      in int expectedPacketLength
      )
    {
      Assert.AreEqual(expectedPackageMode, packetToTestOn.SubMode);
      Assert.AreEqual(expectedPacketLength, packetToTestOn.NumberOfSubPackets);
    }

    [Test]
    public void Test_DecodeWithOperator_FollowedByTotalLengthOfBits()
    {
      var packet = new Packet("00111000000000000110111101000101001010010001001000000000");
      Assert_NumberOfSubPacket_PacketLength(
        packetToTestOn: packet,
        expectedPackageMode: SubPackageMode.LengthInBits,
        expectedPacketLength: 2
        );
      AssertFor_Id_Version_PacketLength_LiteralValue_LiteralBool(
        packetToTestOn: packet,
        expectedVersion: 1,
        expectedId: 6,
        expectedLiteralBool: false,
        expectedPacketLength: 49,
        expectedLiteralValue: 0
        );
    }

    [Test]
    public void Test_DecodeWithOperator_FollowedByNumberOfSubPackets()
    {
      var packet = new Packet("11101110000000001101010000001100100000100011000001100000");
      Assert_NumberOfSubPacket_PacketLength(
        packetToTestOn: packet,
        expectedPackageMode: SubPackageMode.NumberOfPackages,
        expectedPacketLength: 3
        );
      AssertFor_Id_Version_PacketLength_LiteralValue_LiteralBool(
        packetToTestOn: packet,
        expectedVersion: 7,
        expectedId: 3,
        expectedLiteralBool: false,
        expectedPacketLength: 51,
        expectedLiteralValue: 0
        );
    }


    [Test]
    [TestCaseSource(nameof(TestCases_GetBinaryFromHex))]
    public void Test_GetBinaryFromHex(string hex, string expectedBinary)
    {
      // Act
      string actualBinary = Packet.GetBinaryFromHex(hex);
      // Assert
      Assert.AreEqual(expectedBinary, actualBinary);
    }

    [Test]
    [TestCaseSource(nameof(TestCases_GetSumOfVersion))]
    public void Test_GetSumOfVersion(string binary, int expectedVersionTotal)
    {
      // Set up
      var actualPacket = new Packet(binary);
      // Act
      int actualVersionTotal = actualPacket.GetSumOfVersion();
      // Assert
      Assert.AreEqual(expectedVersionTotal, actualVersionTotal);
    }

    [Test]
    [TestCaseSource(nameof(TestCases_GetSumOfVersion_WithHex))]
    public void Test_GetSumOfVersion_WithHex(string hex, int expectedVersionTotal)
    {

      // Set up
      string actualBinary = Packet.GetBinaryFromHex(hex);
      var actualPacket = new Packet(actualBinary);
      // Act
      int actualVersionTotal = actualPacket.GetSumOfVersion();
      // Assert
      Assert.AreEqual(expectedVersionTotal, actualVersionTotal);
    }

    [Test]
    [TestCaseSource(nameof(TestCases_ActualValue))]
    public void Test_ActualValue(string hex, int expectedActualValue)
    {
      // Set up
      string actualBinary = Packet.GetBinaryFromHex(hex);
      var actualPacket = new Packet(actualBinary);
      // Act
      var actualValue = actualPacket.ActualValue;
      // Assert
      Assert.AreEqual(expectedActualValue, actualValue);
    }

    public static object[] TestCases_GetBinaryFromHex
      => new object[]
      {
        new object[] { "AF", "10101111"},
        new object[] { "FFFF", "1111111111111111" },
        new object[] { "0", "0000"},
        new object[] { "12B", "000100101011" }
      };

    public static object[] TestCases_GetSumOfVersion
      => new object[]
      {
        new object[] { "110100101111111000101000", 6},
        new object[] { "11101110000000001101010000001100100000100011000001100000", 14 },
        new object[] { "00111000000000000110111101000101001010010001001000000000", 9},
      };


    public static object[] TestCases_GetSumOfVersion_WithHex
      => new object[]
      {
        new object[] { "8A004A801A8002F478", 16},
        new object[] { "620080001611562C8802118E34", 12},
        new object[] { "C0015000016115A2E0802F182340", 23},
        new object[] { "A0016C880162017C3686B18A3D4780", 31},
      };

    public static object[] TestCases_ActualValue
      => new object[]
      {
            new object[] { "C200B40A82", 3},
            new object[] { "04005AC33890", 54},
            new object[] { "880086C3E88112", 7},
            new object[] { "CE00C43D881120", 9},
            new object[] { "D8005AC2A8F0", 1},
            new object[] { "F600BC2D8F", 0},
            new object[] { "9C005AC2F8F0", 0},
            new object[] { "9C0141080250320F1802104A08", 1},
            
      };



  }
}