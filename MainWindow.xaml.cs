using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using Avalonia.Threading;
using System;
using System.Threading;

namespace TooltipTest
{
    public class MainWindow : Window
    {
        private ToolTip tooltip;

        public MainWindow()
        {
            InitializeComponent();

            tooltip = new ToolTip { IsVisible = false, Content = "I am ToolTip." };

            var t = new Thread(DoBackgroundWork);
            t.IsBackground = true;
            t.Start();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected virtual void OnSetToolTip(string text)
		{
			//if (this.ToolTip is ToolTip tooltip)
			{
				if (global::Avalonia.Controls.ToolTip.GetTip(this) != tooltip)
				{
					global::Avalonia.Controls.ToolTip.SetTip(this, tooltip);
				}

				if (string.IsNullOrWhiteSpace(text))
				{
					global::Avalonia.Controls.ToolTip.SetIsOpen(this, false);
					//tooltip.IsVisible = false;
				}
				else
				{
					//if (!string.Equals(text, tooltip.Content as string))
					//	tooltip.Content = text;
					if (!tooltip.IsVisible)
						tooltip.IsVisible = true;
					global::Avalonia.Controls.ToolTip.SetIsOpen(this, true);
				}
			}
		}

        protected override void OnPointerMoved(PointerEventArgs e)
        {
            OnSetToolTip((new Random().Next() % 2) == 1 ? "show" : "");
        }

        private void DoBackgroundWork(object nullState)
        {
            while(true)
            {
                Thread.Sleep(1000);
                Dispatcher.UIThread.Post(() => this.Content = DateTime.UtcNow.Ticks.ToString());
            }
        }

        
    }
}
