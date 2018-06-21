using Reactive.Bindings;

namespace Ctrl2CapsIsBetterLife
{
	internal class ViewModel
	{
		public ReactiveProperty<int> CtrlCount { get; } = new ReactiveProperty<int>();

		public ReactiveProperty<int> CapsCount { get; } = new ReactiveProperty<int>();
	}
}