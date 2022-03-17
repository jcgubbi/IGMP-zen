namespace ICMP_test
{
	using ZenLib;
	using static ZenLib.Language;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Diagnostics.CodeAnalysis;

	public sealed class RouterState
	{
		/// <summary>
		/// Set of boolean variables to simulate router state
		/// </summary>
		public bool Dst_in_routing_table;
		public bool Process_port_not_active;
		public bool Reassembly_time_exceeded;
		public bool No_buffer_space;
		public bool Discard_dg; ///????
		public bool Frag_zero_na;
		public bool To_fragment_dg;
		public bool Header_problem;
		public bool Dtg_fast;
		public bool Same_network;
		public HostState Host_state;
		public bool Host_query;
        public bool Host_report;

		public Zen<RouterState> Create(
			Zen<bool> dst_in_routing_table,
			Zen<bool> process_port_not_active,
			Zen<bool> reassembly_time_exceeded,
			Zen<bool> no_buffer_space,
			Zen<bool> discard_dg,
			Zen<bool> frag_zero_na,
			Zen<bool> to_fragment_dg,
			Zen<bool> header_problem,
			Zen<bool> dtg_fast,
			Zen<bool> same_net,
			Zen<HostState> host_state,
			Zen<bool> host_query,
			Zen<bool> host_report)
		{
			return Language.Create<RouterState>(
				("Dst_in_routing_table", dst_in_routing_table),
				("Process_port_not_active", process_port_not_active),
				("Reassembly_time_exceeded", reassembly_time_exceeded),
				("No_buffer_space", no_buffer_space),
				("Discard_dg", discard_dg),
				("Frag_zero_na", frag_zero_na),
				("To_fragment_dg", to_fragment_dg),
				("Header_problem", header_problem),
				("Dtg_fast:", dtg_fast),
				("Same_network", same_net),
				("Host state", host_state),
				("Host query", host_query),
				("Host report", host_report));
		}

		public override string ToString()
		{
			return $"Destination in Routing Table: {Dst_in_routing_table}\n" +
				$"Process Port Not Active: {Process_port_not_active}\n" +
				$"Reassembly Time Exceeded: {Reassembly_time_exceeded}\n" +
				$"No buffer space: {No_buffer_space}\n" +
				$"Discard Datagram: {Discard_dg}\n" +
				$"Fragment Zero Not Available: {Frag_zero_na}\n" +
				$"Datagram to be fragmented: {To_fragment_dg}\n" +
				$"Header parameter problem: {Header_problem}\n" +
				$"Datagrams arriving too fast: {Dtg_fast}\n" +
				$"Host and next gateway on same network: {Same_network}\n" +
				$"Host state: {Host_state}\n" +
				$"Host query: {Host_query}\n" +
				$"Host report: {Host_report}\n"
            ;
        }
	}

	public static class RouterStateExtensions
	{
		public static Zen<bool> GetDstRt(this Zen<RouterState> rs) => rs.GetField<RouterState, bool>("Dst_in_routing_table");

		public static Zen<bool> GetPortNA(this Zen<RouterState> rs) => rs.GetField<RouterState, bool>("Process_port_not_active");

		public static Zen<bool> GetReassemblyTime(this Zen<RouterState> rs) => rs.GetField<RouterState, bool>("Reassembly_time_exceeded");

		public static Zen<bool> GetNoBuffer(this Zen<RouterState> rs) => rs.GetField<RouterState, bool>("No_buffer_space");

		public static Zen<bool> GetDiscardDg(this Zen<RouterState> rs) => rs.GetField<RouterState, bool>("Discard_dg");

		public static Zen<bool> GetFragNA(this Zen<RouterState> rs) => rs.GetField<RouterState, bool>("Frag_zero_na");


		public static Zen<bool> GetToFrag(this Zen<RouterState> rs) => rs.GetField<RouterState, bool>("To_fragment_dg");

		public static Zen<bool> GetHeadProb(this Zen<RouterState> rs) => rs.GetField<RouterState, bool>("Header_problem");

		public static Zen<bool> GetDtgFast(this Zen<RouterState> rs) => rs.GetField<RouterState, bool>("Dtg_fast");

		public static Zen<bool> GetSameNet(this Zen<RouterState> rs) => rs.GetField<RouterState, bool>("Same_network");

		public static Zen<HostState> GetHostState(this Zen<RouterState> rs) => rs.GetField<RouterState, HostState>("Host_state");
	}
}
