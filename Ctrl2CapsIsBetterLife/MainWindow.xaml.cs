using System.Windows;
using System.Windows.Input;
using Gma.System.MouseKeyHook;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;

namespace Ctrl2CapsIsBetterLife
{
	public partial class MainWindow : Window
	{
		private readonly ViewModel _vm;

		public MainWindow()
		{
			InitializeComponent();

			var countData = CountData.GetCurrent();
			_vm = new ViewModel();
			_vm.CtrlCount.Value = countData.Ctrl;
			_vm.CapsCount.Value = countData.Caps;
			DataContext = _vm;

			var globalHook = Hook.GlobalEvents();
			globalHook.KeyUp += GlobalHook_KeyUp;
			globalHook.KeyDown += GlobalHook_KeyDown;
		}

		private void GlobalHook_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyValue == 162)
			{
				_vm.CtrlCount.Value++;
				WriteCurrent();
			}
		}

		private void GlobalHook_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyValue == 240)
			{
				_vm.CapsCount.Value++;
				WriteCurrent();
			}
		}

		private void WriteCurrent()
		{
			new CountData {Ctrl = _vm.CtrlCount.Value, Caps = _vm.CapsCount.Value}.WriteCurrent();
		}

		private void MainWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			DragMove();
		}

		private void CloseButton_OnClick(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void ResetButton_OnClick(object sender, RoutedEventArgs e)
		{
			CountData.ResetCurrent();
			_vm.CtrlCount.Value = 0;
			_vm.CapsCount.Value = 0;
		}

		private void HistoryButton_OnClick(object sender, RoutedEventArgs e)
		{
			System.Diagnostics.Process.Start(CountData.FilePath);
		}
	}
}