// See https://aka.ms/new-console-template for more information



    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;
    using ICMP_test;
    using ZenLib;
    using static ZenLib.Language;

/*
ZenFunction<Datagram, byte> f = new ZenFunction<Datagram, byte>(IcmpModel.EchoAndEchoReply);

foreach (var l in f.GenerateInputs())
{
    Console.WriteLine(l);
}
*/





/*ZenFunction<Datagram, RouterState, byte> f2 = new ZenFunction<Datagram, RouterState, byte>(IcmpModel.TimeExceededMessage);
foreach (var l in f2.GenerateInputs())
{
    Console.WriteLine(l);
}*/


/*
ZenFunction<Datagram, RouterState, byte> f3 = new ZenFunction<Datagram, RouterState, byte>(IcmpModel.DestinationUnreachable);
foreach (var l in f3.GenerateInputs())
{
    Console.WriteLine(l);
}
*/

/*
ZenFunction<Datagram, RouterState, byte> f4 = new ZenFunction<Datagram, RouterState, byte>(IcmpModel.ParameterProblemMessage);
foreach (var l in f4.GenerateInputs())
{
    Console.WriteLine(l);
}
*/




int count = 0;
// creating Zen function for IcmpMain()
ZenFunction<Datagram, RouterState, IList<byte>> f5 = new ZenFunction<Datagram, RouterState, IList<byte>>(IcmpModel.IcmpMain);
foreach (var l in f5.GenerateInputs()) // generate all possible inputs
{
    //Console.WriteLine("\n\n"+l);
    //Console.WriteLine("output : "+f5.Evaluate(l.Item1,l.Item2));
    //Console.WriteLine(out);
    //Console.WriteLine(If<string>(f5.Evaluate(l.Item1, l.Item2) == 1, l, " "));

    //if (f5.Evaluate(l.Item1, l.Item2) == 1)
    var err = f5.Evaluate(l.Item1, l.Item2); // for printing the kind of error in this particular input
    Console.WriteLine("\n\n" + l + "output : " + err[0]+err[1]+err[2]+err[3]+err[4]+err[5]+err[6]+err[7]); 
    count++;
    
}
Console.WriteLine("@@ Total cases : "+count);









/*
ZenFunction<int, int, int> f = new ZenFunction<int, int, int>(IcmpModel.MultiplyAndAdd2);

foreach(var l in f.GenerateInputs())
{
    Console.WriteLine(l);
}
*/
