namespace ICMP_test
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using ZenLib;
    using static ZenLib.Language;

    /// <summary>
    /// A Resource Record object.
    /// </summary>
    public sealed class Datagram
    {
        /// <summary>
        /// Source IP address
        /// </summary> 
        public uint SrcIp;

        /// <summary>
        /// Destination IP address
        /// </summary>
        public uint DstIp;

        /// <summary>
        /// Time to live
        /// </summary>
        public byte TTL;

        /// <summary>
        /// Do not fragment flag
        /// </summary>
        public bool DF;

        /// <summary>
        /// Checksum
        /// </summary>
        public ushort Csum;

        /// <summary>
        /// header length
        /// </summary>
        public byte HLen;


        public uint Options;

        /// <summary>
        /// ICMP Data
        /// </summary>
        public IcmpPacket Data;

        /// <summary>
        /// Create a Zen RR from a three tuple.
        /// </summary>
        /// <param name="SrcIp">The record name.</param>
        /// <param name="DstIp">The record type.</param>
        /// <param name="TTL">The record data.</param>
        /// <param name="DF">The Do Not Fragment flag</param>
        /// <param name="Csum">Checksum</param>
        /// <param name="HLen">Header length</param>
        /// <param name="Options">Options (if any)</param>
        /// <param name="Data">The ICMP Payload</param>
        /// <returns>A Zen IP Datagram.</returns>
        public static Zen<Datagram> Create(
            Zen<uint> src_ip,
            Zen<uint> dst_ip,
            Zen<byte> ttl,
            Zen<bool> df,
            Zen<ushort> csum,
            Zen<byte> hlen,
            Zen<uint> opt,
            Zen<IcmpPacket> data)
        {
            return Language.Create<Datagram>(
                ("SrcIp", src_ip),
                ("DstIp", dst_ip),
                ("TTL", ttl),
                ("DF", df),
                ("Csum", csum),
                ("HLen", hlen),
                ("Options", opt),
                ("Data", data));
        }

        /// <summary>
        ///     Equality for domain names.
        /// </summary>
        /// <param name="other">The other IP packet.</param>
        /// <returns>True or false.</returns>
        public bool Equals(Datagram other)
        {
            return SrcIp.Equals(other.SrcIp) &&
                DstIp.Equals(other.DstIp) &&
                TTL.Equals(other.TTL) &&
                DF.Equals(other.DF) &&
                Csum.Equals(other.Csum) &&
                HLen.Equals(other.HLen) &&
                Options.Equals(other.Options) &&
                Data.Equals(other.Data);
        }

        /// <summary>
        /// Convert the resource record to a string format.
        /// </summary>
        /// <returns>The string.</returns>
        public override string ToString()
        {
            return $"Src: {SrcIp}\nDst: {DstIp}\nTTL: {TTL}\nDF: {DF}\nChecksum: {Csum}\nHeader Length: {HLen}\nOptions: {Options}\nData: {Data}";
        }
    }

    /// <summary>
    /// Datagram Zen extension methods.
    /// </summary>
    public static class DatagramExtensions
    {
        /// <summary>
        /// Gets the source ip of the Datagram.
        /// </summary>
        /// <param name="dg">The datagram.</param>
        /// <returns>The source ip of the datagram.</returns>
        public static Zen<uint> GetSrcIp(this Zen<Datagram> dg) => dg.GetField<Datagram, uint>("SrcIp");

        /// <summary>
        /// Gets the dest ip of the datagram.
        /// </summary>
        /// <param name="dg">The datagram.</param>
        /// <returns>The dest ip of the datagram.</returns>
        public static Zen<uint> GetDstIp(this Zen<Datagram> dg) => dg.GetField<Datagram, uint>("DstIp");

        /// <summary>
        /// Gets the TTL field of the datagram.
        /// </summary>
        /// <param name="dg">The datagram.</param>
        /// <returns>The ttl of the datagram.</returns>
        public static Zen<byte> GetTTL(this Zen<Datagram> dg) => dg.GetField<Datagram, byte>("TTL");

        /// <summary>
        /// Gets the DF field of the datagram.
        /// </summary>
        /// <param name="dg">The datagram.</param>
        /// <returns>The DF of the datagram.</returns>
        public static Zen<bool> GetDF(this Zen<Datagram> dg) => dg.GetField<Datagram, bool>("DF");

        /// <summary>
        /// Gets the Csum field of the datagram.
        /// </summary>
        /// <param name="dg">The datagram.</param>
        /// <returns>The Csum of the datagram.</returns>
        public static Zen<ushort> GetCsum(this Zen<Datagram> dg) => dg.GetField<Datagram, ushort>("Csum");

        /// <summary>
        /// Gets the Hlen field of the datagram.
        /// </summary>
        /// <param name="dg">The datagram.</param>
        /// <returns>The Hlen of the datagram.</returns>
        public static Zen<byte> GetHlen(this Zen<Datagram> dg) => dg.GetField<Datagram, byte>("Hlen");

        /// <summary>
        /// Gets the Options field of the datagram.
        /// </summary>
        /// <param name="dg">The datagram.</param>
        /// <returns>The options of the datagram.</returns>
        public static Zen<uint> GetOptions(this Zen<Datagram> dg) => dg.GetField<Datagram, uint>("Options");

        public static Zen<IcmpPacket> GetICMPPacket(this Zen<Datagram> dg) => dg.GetField<Datagram, IcmpPacket>("Data"); 
        
    }
}