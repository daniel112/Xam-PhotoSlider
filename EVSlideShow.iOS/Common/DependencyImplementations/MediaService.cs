﻿using System;
using System.Threading.Tasks;
using CoreGraphics;
using EVSlideShow.Core.Components.Common.DependencyInterface;
using EVSlideShow.iOS.Common.DependencyImplementations;
using Foundation;
using GMImagePicker;
using Photos;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(MediaService))]
namespace EVSlideShow.iOS.Common.DependencyImplementations {
    public class MediaService : IMediaService {

        private GMImagePickerController _PickerController;
        private GMImagePickerController PickerController {
            get {
                if (_PickerController == null) {
                    _PickerController = new GMImagePickerController {
                        GridSortOrder = SortOrder.Descending,
                        Title = "Select Up To 5 Images",
                        DisplaySelectionInfoToolbar = true,
                        //NavigationBarBackgroundColor = UIColor.FromRGB(42, 52, 68),
                        NavigationBarTextColor = UIColor.White,
                        NavigationBarTintColor = UIColor.White,
                        PickerTextColor = UIColor.White,
                        ToolbarBarTintColor = UIColor.DarkGray,
                        ToolbarTextColor = UIColor.White,
                        ToolbarTintColor = UIColor.Red,
                        PickerStatusBarStyle = UIStatusBarStyle.LightContent,
                        PickerBackgroundColor = UIColor.FromRGB(42, 52, 68),

                    };
                }
                _PickerController.ShouldSelectAsset += PickerController_ShouldSelectAsset;
                _PickerController.FinishedPickingAssets += PickerController_FinishedPickingAssets;
                _PickerController.Canceled += (object sender, EventArgs e) => {
                    Console.WriteLine("user canceled picking assets");
                };
                return _PickerController;

            }



        }

        public MediaService() {
        }

        public void OpenGallery() {

            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(PickerController, true, null);

        }

        #region Delegates
        void PickerController_ShouldSelectAsset(object sender, CancellableAssetEventArgs args) {
            var controller = (GMImagePickerController)sender;
            args.Cancel = controller.SelectedAssets.Count >= 5;

        }
        void PickerController_FinishedPickingAssets(object sender, MultiAssetEventArgs args) {

            foreach (var asset in args.Assets) {
                PHImageManager imageManager = new PHImageManager();
                imageManager.RequestImageForAsset(asset,
                    new CGSize(asset.PixelWidth, asset.PixelHeight),
                    PHImageContentMode.Default,
                    null,
                    (image, info) => {
                    // TODO: convert to something and pass to forms to upload
                        var imageToSend = image.AsJPEG().GetBase64EncodedString(NSDataBase64EncodingOptions.None);
                    });
            }

        }


        #endregion
    }
}
