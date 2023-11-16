#import <SafariServices/SafariServices.h>

extern UIViewController* UnityGetGLViewController();

extern "C"
{
    void launchUrl(const char *url)
    {
        
        UIViewController *uvc = UnityGetGLViewController();

        NSURL *URL = [NSURL URLWithString:[[NSString alloc] initWithUTF8String:url]];

        SFSafariViewController *sfvc = [[SFSafariViewController alloc] initWithURL:URL];

        [uvc presentViewController:sfvc animated:YES completion:nil];
    }
    void dismiss()
     {
       UIViewController *uvc = UnityGetGLViewController();
       [uvc dismissViewControllerAnimated:YES completion:nil];
     }
}
