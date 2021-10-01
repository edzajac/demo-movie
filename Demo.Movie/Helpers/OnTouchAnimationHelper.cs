
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Demo.Movie.Helpers
{
    public static class OnTouchAnimationHelper
    {
        public static Task<bool> ScaleDownTo(this View view, double scaleSize)
        {
            if (view != null)
            {
                return view.ScaleTo(scaleSize, 50, Easing.SpringIn);
            }

            return null;
        }

        public static Task<bool> ScaleToOriginalSize(this View view)
        {
            if (view != null)
            {
                return view.ScaleTo(1, 150, Easing.Linear);
            }

            return null;
        }
    }
}
