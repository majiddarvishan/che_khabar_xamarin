using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CheKhabar.Controls.BorderlessEntry), typeof(CheKhabar.iOS.BorderlessEntryRenderer))]

namespace CheKhabar.iOS
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (this.Control != null)
            {
                this.Control.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}