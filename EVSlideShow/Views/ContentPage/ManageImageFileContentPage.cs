﻿using System;
using System.Collections.Generic;
using EVSlideShow.Core.Common;
using EVSlideShow.Core.Components.Common.DependencyInterface;
using EVSlideShow.Core.Components.Helpers;
using EVSlideShow.Core.Constants;
using EVSlideShow.Core.Models;
using EVSlideShow.Core.ViewModels;
using EVSlideShow.Core.Views.Base;
using EVSlideShow.Core.Views.ContentViews;
using Plugin.Permissions.Abstractions;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace EVSlideShow.Core.Views {
    public class ManageImageFileContentPage : BaseContentPage<ManageImageFileViewModel>, IImageButtonDelegate, IInputButtonPopupPage {

        #region Variables
        private ScrollView _ScrollViewContent;
        private ScrollView ScrollViewContent {
            get {
                if (_ScrollViewContent == null) {
                    _ScrollViewContent = new ScrollView {
                        Margin = new Thickness(20),
                        HorizontalOptions = LayoutOptions.CenterAndExpand
                    };
                }
                return _ScrollViewContent;
            }
        }

        private StackLayout _StackLayoutImageTitle;
        private StackLayout StackLayoutImageTitle {
            get {
                if (_StackLayoutImageTitle == null) {
                    _StackLayoutImageTitle = new StackLayout {
                        Orientation = StackOrientation.Horizontal
                    };
                }
                return _StackLayoutImageTitle;
            }
        }

        private FlexLayout _FlexLayoutMainContent;
        private FlexLayout FlexLayoutMainContent {
            get {
                if (_FlexLayoutMainContent == null) {
                    _FlexLayoutMainContent = new FlexLayout {
                        Direction = FlexDirection.Column,
                        JustifyContent = FlexJustify.Center,
                    };
                }
                return _FlexLayoutMainContent;
            }
        }

        private Image _ImageContentManage;
        private Image ImageContentManage {
            get {
                if (_ImageContentManage == null) {
                    _ImageContentManage = new Image {
                        Aspect = Aspect.AspectFit,
                        Source = "icon_manage",
                        WidthRequest = 60,
                        HeightRequest = 60,
                        HorizontalOptions = LayoutOptions.Center,
                    };
                }
                return _ImageContentManage;
            }
        }

        private ContentView _ContentViewImage;
        private ContentView ContentViewImage {
            get {
                if (_ContentViewImage == null) {
                    _ContentViewImage = new ContentView {
                    };
                }
                return _ContentViewImage;
            }
        }

        private Label _LabelInstruction;
        private Label LabelInstruction {
            get {
                if (_LabelInstruction == null) {
                    _LabelInstruction = new Label {
                        Text = "Type and save the below URL in your Tesla screen to access slideshows and free content",
                        LineBreakMode = LineBreakMode.WordWrap,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 18,
                        TextColor = Color.White,
                        BackgroundColor = Color.Transparent,
                        Margin = new Thickness(20, 20, 20, 0),
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                    };
                    _LabelInstruction.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily_Bold);

                }

                return _LabelInstruction;
            }
        }

        private Label _LabelURL;
        private Label LabelURL {
            get {
                if (_LabelURL == null) {
                    _LabelURL = new Label {
                        Text = "https://evslideshow.com/login",
                        HorizontalTextAlignment = TextAlignment.Center,
                        LineBreakMode = LineBreakMode.WordWrap,
                        FontSize = 20,
                        TextColor = Color.White,
                        FontAttributes = FontAttributes.Bold,
                        BackgroundColor = Color.Transparent,
                        Margin = new Thickness(10, 20, 10, 0),
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                    };
                    _LabelURL.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily_Bold);

                }

                return _LabelURL;
            }
        }

        private Label _LabelMessage;
        private Label LabelMessage {
            get {
                if (_LabelMessage == null) {
                    _LabelMessage = new Label {
                        Text = "Select up to 30 photos to display within EV Slideshow. Upload 5 photos at a time and wait for confirmation",
                        LineBreakMode = LineBreakMode.WordWrap,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 18,
                        TextColor = Color.White,
                        BackgroundColor = Color.Transparent,
                        Margin = new Thickness(20, 20, 20, 0),
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                    };
                    _LabelMessage.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily_Bold);

                }

                return _LabelMessage;
            }
        }

        private ImageButtonContentView _ImageButtonUploadPhotos;
        private ImageButtonContentView ImageButtonUploadPhotos {
            get {
                if (_ImageButtonUploadPhotos == null) {
                    _ImageButtonUploadPhotos = new ImageButtonContentView("Upload", this);
                }
                return _ImageButtonUploadPhotos;
            }
        }

        private ImageButtonContentView _ImageButtonDeletePhotos;
        private ImageButtonContentView ImageButtonDeletePhotos {
            get {
                if (_ImageButtonDeletePhotos == null) {
                    _ImageButtonDeletePhotos = new ImageButtonContentView("Delete", this);
                }
                return _ImageButtonDeletePhotos;
            }
        }

        private Button _ButtonSubscribe;
        private Button ButtonSubscribe {
            get {
                if (_ButtonSubscribe == null) {
                    _ButtonSubscribe = new Button {
                        Text = "SUBSCRIBE",
                        FontSize = 18,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.Black,
                        BackgroundColor = Color.White,
                        CornerRadius = 8,
                        HeightRequest = 50,
                        Margin = new Thickness(0, 30, 0, 0),

                    };
                    _ButtonSubscribe.Clicked += ButtonSubscribe_Clicked;
                    _ButtonSubscribe.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily_Bold);

                }
                return _ButtonSubscribe;
            }
        }

        private Grid _GridButtons;
        private Grid GridButtons {
            get {
                if (_GridButtons == null) {
                    _GridButtons = new Grid {
                        Margin = new Thickness(10, 20, 10, 0),
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        RowDefinitions = {
                            new RowDefinition { Height = new GridLength(50, GridUnitType.Absolute) }, // row 0                        
                            new RowDefinition { Height = GridLength.Star }, // row 1
                         },
                        ColumnDefinitions = {
                            new ColumnDefinition { Width =  new GridLength(1, GridUnitType.Star) }, // col 0
                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }, // col 1
                        },
                    };
                }
                return _GridButtons;
            }
        }

        private ToolbarItem _ToolbarUser;
        private ToolbarItem ToolbarUser {
            get {
                if (_ToolbarUser == null) {
                    _ToolbarUser = new ToolbarItem {
                        Icon = "icon_user"
                    };
                    _ToolbarUser.Clicked += ToolbarUser_ClickedAsync;
                }

                return _ToolbarUser;
            }
        }
        
        #endregion

        #region Initialization
        public ManageImageFileContentPage() {
        }

        public ManageImageFileContentPage(User user) {
            this.ViewModel.User = user;
            MessagingCenterSubscribe();
            this.Setup();

        }
        protected override void OnOrientationUpdate(DeviceOrientatione orientation) {
        }

        protected override void OnAppearing() {
            base.OnAppearing();
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
        }

        #endregion

        #region Private API
        private void Setup() {

            Title = "Managing Slideshow #1";
            // image
            this.ContentViewImage.Content = this.ImageContentManage;


            // Toolbar items
            this.ToolbarItems.Add(ToolbarUser);


            // GridButtons
            // grid.Children.Add(item ,col, col+colSpan, row, row+rowspan)
            this.GridButtons.Children.Add(ImageButtonUploadPhotos, 0, 1, 0, 1);
            this.GridButtons.Children.Add(ImageButtonDeletePhotos, 1, 2, 0, 1);
            this.GridButtons.Children.Add(ButtonSubscribe, 0, 2, 1, 2);


            // Label wrapping is buggy, so we put the wrapped label in 1x1 grid
            var gridSummary = new Grid {
                RowDefinitions = {
                            new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }, // row 0                        
                            new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }, // row 1
                         }
            };
            gridSummary.Children.Add(this.LabelURL, 0, 0);
            gridSummary.Children.Add(this.LabelMessage, 0, 1);

            // flexlayout
            this.FlexLayoutMainContent.Children.Add(this.ContentViewImage);
            this.FlexLayoutMainContent.Children.Add(this.LabelInstruction);
            this.FlexLayoutMainContent.Children.Add(gridSummary);
            this.FlexLayoutMainContent.Children.Add(this.GridButtons);

            this.ScrollViewContent.Content = new StackLayout {
                Children = {
                    this.FlexLayoutMainContent,

                }
            };

            Content = this.ScrollViewContent;

        }

        #region MessagingCenter

        private void MessagingCenterSubscribe() {
            MessagingCenter.Subscribe<List<string>>(this, MessagingKeys.DidFinishSelectingImages, MessagingCenter_SendToCropView);

        }
        private void MessagingCenterUnsubscribe() {
            MessagingCenter.Unsubscribe<List<string>>(this, MessagingKeys.DidFinishSelectingImages);
        }

        void MessagingCenter_SendToCropView(object obj) {
            List<string> encodedImages = (List<string>)obj;
            Console.WriteLine($"{encodedImages.Count} images to crop");
            this.Navigation.PushAsync(new ImageCroppingContentPage(encodedImages, this.ViewModel.User, this.ViewModel.SlideShowNumber));



        }
        #endregion

        #region Buttons

        void ButtonSubscribe_Clicked(object sender, EventArgs e) {


        }

        async void ToolbarUser_ClickedAsync(object sender, EventArgs e) {

            var option = await DisplayActionSheet("Select Slideshow", "Cancel", null, "Slideshow #1", "Slideshow #2", "Slideshow #3", "Logout");
            switch (option) {
                case "Slideshow #1":
                    this.ViewModel.SlideShowNumber = 1;
                    Title = "Managing Slideshow #1";
                    break;
                case "Slideshow #2":
                    this.ViewModel.SlideShowNumber = 2;
                    Title = "Managing Slideshow #2";
                    break;
                case "Slideshow #3":
                    this.ViewModel.SlideShowNumber = 3;
                    Title = "Managing Slideshow #3";
                    break;
                case "Logout":
                    Application.Current.Properties.Clear();
                    await Application.Current.SavePropertiesAsync();
                    Application.Current.MainPage = new IntroContentPage();
                    break;
            }

        }
        #endregion

        #endregion

        #region Public API

        #endregion

        #region Delegates
        async void IImageButtonDelegate.ImageButton_DidPress(string buttonText, ImageButtonContentView button) {
            if (buttonText == "Upload") {
                if (!this.ViewModel.User.IsSubscribed) {
                    await DisplayAlert("No Subscription Found", "Your account is not currently subscribed. Only subscribers have access to photo uploads. You can subscribe by hitting the 'Subscribe' button", "Ok");
                    return;
                }
                var status = await PermissionHelper.GetPermissionStatusForPhotoLibraryAsync();
                if (status == PermissionStatus.Granted) {
                    var mediaServie = DependencyService.Get<IMediaService>();
                    mediaServie.OpenGallery();
                } else {
                    await DisplayAlert("Permission Status", "Please enable access to photo library by going to your app settings.", "Ok");
                }

            } else {
                var action = await DisplayActionSheet("Delete Photos", "Cancel", "Delete All", "Delete by #");
                switch (action) {
                    case "Delete All":
                        // TODO: put loading indicator
                        if (await this.ViewModel.DeleteAll()) {
                            // successful
                            await DisplayAlert("Success", $"All photos for Slideshow #{ViewModel.SlideShowNumber} have been deleted", "Ok");

                        } else {
                            // unsuccessful
                            await DisplayAlert("Error", $"Something went wrong, please try again later.", "Ok");

                        }
                        break;
                    case "Delete by #":
                        var popupPage = new InputButtonPopupPage("Enter slide number(s) separated by commas", "Delete") {
                            PageDelegate = this
                        };
                        await Navigation.PushPopupAsync(popupPage);
                        break;
                }
            }
        }


        async void IInputButtonPopupPage.DidTapButton(string text) {
            Console.WriteLine(text);
            await ViewModel.DeleteByID(text);
        }

        #endregion


    }
}
