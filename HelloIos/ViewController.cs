using System;

using UIKit;
using Foundation;
namespace HelloIos {
  public partial class ViewController: UIViewController {
    protected ViewController( IntPtr handle ) : base( handle ) {
      // Note: this .ctor should not contain any initialization logic.
    }

    string translatedNumber = string.Empty;

    public override void ViewDidLoad() {
      base.ViewDidLoad();
      // Perform any additional setup after loading the view, typically from a nib.
      TranslateButton.TouchUpInside += TranslateTouched;
      CallButton.TouchUpInside += CallButtonTouched;
    }

    void TranslateTouched (Object sender, EventArgs e){
      translatedNumber = PhoneTranslator.ToNumber( PhoneNumberText.Text );

      PhoneNumberText.ResignFirstResponder();

      if ( string.IsNullOrWhiteSpace( translatedNumber ) ) {
        CallButton.SetTitle( "Call ", UIControlState.Normal );
        CallButton.Enabled = false;
      }
      else {
        CallButton.SetTitle( $"Call {translatedNumber}", UIControlState.Normal );
        CallButton.Enabled = true;
      }
    }

    void CallButtonTouched( object sender, EventArgs e ) {
      var url = new NSUrl( "tel:" + translatedNumber );
      if(!UIApplication.SharedApplication.OpenUrl(url)){
        var alert = UIAlertController.Create( "Not Supported", "Telephone is not supported", UIAlertControllerStyle.Alert );
        alert.AddAction( UIAlertAction.Create( "Ok", UIAlertActionStyle.Default, null ) );
        PresentViewController( alert, true, null );
      }
    }

    public override void DidReceiveMemoryWarning() {
      base.DidReceiveMemoryWarning();
      // Release any cached data, images, etc that aren't in use.
    }
  }
}
