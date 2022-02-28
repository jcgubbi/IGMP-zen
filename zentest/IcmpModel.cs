namespace ICMP_test
{
    using System.Collections.Generic;
    using System.Linq;
    using ZenLib;
    using System;
    using static ZenLib.Language;

    /// <summary>
    /// Class representing the model for DNS authoritative server.
    /// </summary>
    public sealed class IcmpModel
    {

        // Below are the implementations of ICMP err messages, with all possible branches that give rise to error message 
        public static Zen<byte> DestinationUnreachable(Zen<Datagram> dtg, Zen<RouterState> rs)
        {
            var df = dtg.GetDF();
            var dstrt = rs.GetDstRt();
            var ppnact = rs.GetPortNA();
            var tofrag = rs.GetToFrag();
            var b0 = (Zen<byte>)0;
            var zbt = (Zen<bool>)True();
            var frt = rs.GetReassemblyTime();
            var fna = rs.GetFragNA();

            return If<byte>(
                dstrt == False(),
                1,
                If<byte>(
                    ppnact == True(),
                    1,
                    If<byte>(
                        And(tofrag == True(), df == True()),
                        1,
                        0)
                    )
                );
        }
        
        public static Zen<byte> TimeExceededMessage(Zen<Datagram> dtg, Zen<RouterState> rs)
        {
            //TTL == 0 : Discard + TEM
            //Fragment reassembly time exceeded : Discard + TEM
            //Fragment 0 not available : Do not send TEM
            var ttl = dtg.GetTTL();
            var b0 = (Zen<byte>)0;
            var zbt = (Zen<bool>)True();
            var frt = rs.GetReassemblyTime();
            var fna = rs.GetFragNA();

            /*return If<byte>(
                Or(ttl == b0, frt == zbt),
                0,
                1);
            */
            return If<byte>(
                ttl!=b0,
                If<byte>(
                    frt!=zbt,
                    0,
                    2),
                2);
        }

        public static Zen<int> MultiplyAndAdd(Zen<int> x, Zen<int> y)
        {
            return If<int>(x > 0, 0, 1);
        }

        public static Zen<int> MultiplyAndAdd2(Zen<int> x, Zen<int> y)
        {
            return If<int>(
                x > 0, 
                0, 
                If<int>(
                    y<10,
                    3,
                    6));
        }



        public static Zen<byte> ParameterProblemMessage(Zen<Datagram> dtg, Zen<RouterState> rs)
        {
            var hp = rs.GetHeadProb();
            return If<byte>(
                hp==True(),
                3,
                0);
        }

        public static Zen<byte> SourceQuenchMessage(Zen<Datagram> dtg, Zen<RouterState> rs)
        {
            var nobuff = rs.GetNoBuffer();
            var tufst = rs.GetDtgFast();

            return If<byte>(
                nobuff == False(), // buffer is there
                If<byte>(tufst == False(),
                    0,
                    4),
                4);

        }
        

        public static Zen<byte> RedirectMessage(Zen<Datagram> dtg, Zen<RouterState> rs)
        {
            var sn = rs.GetSameNet();
            return If<byte>(sn==True(), 5, 0);
        }

        public static Zen<byte> EchoAndEchoReply(Zen<Datagram> dtg, Zen<RouterState> rs)
        {
            // Type should be 8
            var t = dtg.GetICMPPacket().GetTyp();
            // Code should be 0
            var b8 = (Zen<byte>)8;
            var b0 = (Zen<byte>)0;
            var us0 = (Zen<ushort>)0;
            var ui0 = (Zen<uint>)0;
            var cd = dtg.GetICMPPacket().GetCode();
            // CS = 0
            var cs = dtg.GetICMPPacket().GetCsum();
            // Code = 0 => ID = 0
            var sn = dtg.GetICMPPacket().GetSeqNum();
            // Code = 0 => SeqNum = 0
            var id = dtg.GetICMPPacket().GetIdentifier();

            //return If<byte>(dtg.GetICMPPacket().GetTyp() == t0, 1, 0);

            //return If<byte>(t == t0, 1, 0);

            /*return If<byte>(
               And(t == b8, cd == b0, cs == us0, sn == ui0, id == ui0),
               1,
               0);*/

            return If<byte>(
                t == b8,
                If<byte>(
                    cd == b0,
                    If<byte>(
                        cs == us0,
                        If<byte>(
                            sn == ui0,
                            If<byte>(
                                id == ui0,
                                0,
                                6),
                            6),
                        6),
                    6
                    ),
                6
                );

        }


        public static Zen<byte> TimestampOrTimestampReplyMessage(Zen<Datagram> dtg, Zen<RouterState> rs)
        {
            var t = dtg.GetICMPPacket().GetTyp();
            // Code should be 0
            var b8 = (Zen<byte>)13;
            var b0 = (Zen<byte>)0;
            var us0 = (Zen<ushort>)0;
            var ui0 = (Zen<uint>)0;
            var cd = dtg.GetICMPPacket().GetCode();
            // CS = 0
            var cs = dtg.GetICMPPacket().GetCsum();
            // Code = 0 => ID = 0
            var sn = dtg.GetICMPPacket().GetSeqNum();
            // Code = 0 => SeqNum = 0
            var id = dtg.GetICMPPacket().GetIdentifier();

            //return If<byte>(dtg.GetICMPPacket().GetTyp() == t0, 1, 0);

            //return If<byte>(t == t0, 1, 0);

            /*return If<byte>(
               And(t == b8, cd == b0, cs == us0, sn == ui0, id == ui0),
               1,
               0);*/

            return If<byte>(
                t == b8,
                If<byte>(
                    cd == b0,
                    If<byte>(
                        cs == us0,
                        If<byte>(
                            sn == ui0,
                            If<byte>(
                                id == ui0,
                                0,
                                7),
                            7),
                        7),
                    7
                    ),
                7
                );
        }

        public static Zen<byte> InformationOrInformationReplyMessage(Zen<Datagram> dtg, Zen<RouterState> rs)
        {
            var t = dtg.GetICMPPacket().GetTyp();
            // Code should be 0
            var b8 = (Zen<byte>)15;
            var b0 = (Zen<byte>)0;
            var us0 = (Zen<ushort>)0;
            var ui0 = (Zen<uint>)0;
            var cd = dtg.GetICMPPacket().GetCode();
            // CS = 0
            var cs = dtg.GetICMPPacket().GetCsum();
            // Code = 0 => ID = 0
            var sn = dtg.GetICMPPacket().GetSeqNum();
            // Code = 0 => SeqNum = 0
            var id = dtg.GetICMPPacket().GetIdentifier();

            //return If<byte>(dtg.GetICMPPacket().GetTyp() == t0, 1, 0);

            //return If<byte>(t == t0, 1, 0);

            /*return If<byte>(
               And(t == b8, cd == b0, cs == us0, sn == ui0, id == ui0),
               1,
               0);*/

            return If<byte>(
                t == b8,
                If<byte>(
                    cd == b0,
                    If<byte>(
                        cs == us0,
                        If<byte>(
                            sn == ui0,
                            If<byte>(
                                id == ui0,
                                0,
                                8),
                            8),
                        8),
                    8
                    ),
                8
                );
        }

        // IcmpMain() is the wrapper function that takes a datagram, and router state as input and returns all kinds of error which is present in it 

        //public static Zen<byte> IcmpMain(Zen<Datagram> dtg, Zen<RouterState> rs)
        public static Zen<IList<byte>> IcmpMain(Zen<Datagram> dtg, Zen<RouterState> rs)
        {
            var v1 = DestinationUnreachable(dtg, rs);
            var v2 = TimeExceededMessage(dtg, rs);
            var v3 = ParameterProblemMessage(dtg, rs);
            var v4 = SourceQuenchMessage(dtg, rs);
            var v5 = RedirectMessage(dtg, rs);
            var v6 = EchoAndEchoReply(dtg, rs);
            var v7 = TimestampOrTimestampReplyMessage(dtg, rs);
            var v8 = InformationOrInformationReplyMessage(dtg, rs);

            /*
            return If<byte>(
                v1 != 0,
                v1,
                If<byte>(
                    v2!=0,
                    v2,
                    If<byte>(
                        v3!=0,
                        v3,
                        If<byte>(
                            v4!=0,
                            v4,
                            If<byte>(
                                v5!=0,
                                v5,
                                If<byte>(
                                    v6!=0,
                                    v6,
                                    If(
                                        v7!=0,
                                        v7,
                                        v8)))))));*/
            /*
            return If<byte>(
                DestinationUnreachable(dtg,rs) != 0,
                1,
                If<byte>(
                    TimeExceededMessage(dtg,rs) != 0,
                    2,
                    If<byte>(
                        ParameterProblemMessage(dtg,rs) != 0,
                        3,
                        If<byte>(
                            SourceQuenchMessage(dtg,rs) != 0,
                            4,
                            If<byte>(
                                RedirectMessage(dtg,rs) != 0,
                                5,
                                If<byte>(
                                    EchoAndEchoReply(dtg,rs) != 0,
                                    6,
                                    If(
                                        TimestampOrTimestampReplyMessage(dtg,rs) != 0,
                                        7,
                                        InformationOrInformationReplyMessage(dtg,rs))))))));
            */

            //var ret = List<byte> { };

            var d = If<byte>(
                v1 != 0,
                1,
                0);
            var t = If<byte>(
                v2 != 0,
                1,
                0);
            var p = If<byte>(
                v3 != 0,
                1,
                0);


            var r4 = If<byte>(
                v4 != 0,
                1,
                0);
            var r5 = If<byte>(
                v5 != 0,
                1,
                0);
            var r6 = If<byte>(
                v6 != 0,
                1,
                0);
            var r7 = If<byte>(
                v7 != 0,
                1,
                0);
            var r8 = If<byte>(
                v8 != 0,
                1,
                0);

            byte r9 = 5;

            var a = (d * t * p);
            var b = r4 * r5 * r6 * r7 * r8;
            Zen<IList<byte>> err = List(d, t, p, r4, r5, r6, r7, r8);
            //return a*b + a*(1-b);
            //return (r4 * r5 * r6 * r7 * r8);
            return err;
            //Console.WriteLine(v1+v2+v3+v4+v5+v6+v7+v8);
        }
    }
}
