﻿using System;
using EVSlideShow.Core.Common;
using EVSlideShow.Core.Components.CustomRenderers;
using EVSlideShow.Core.Components.Helpers;
using EVSlideShow.Core.Constants;
using EVSlideShow.Core.Models;
using EVSlideShow.Core.Network;
using EVSlideShow.Core.Network.Managers;
using EVSlideShow.Core.ViewModels;
using EVSlideShow.Core.Views.Base;
using EVSlideShow.Core.Views.ContentViews;
using EVSlideShow.Views;
using Xamarin.Forms;

namespace EVSlideShow.Core.Views {
    public class LoginContentPage : BaseContentPage<LoginViewModel>, IInputTextDelegate {

        #region Variables
        private const string InputTextIdentifierUsername = "username";
        private const string InputTextIdentifierPassword = "password";

        private string TextUsername;
        private string TextPassword;
        private string TextEmail;

        private ScrollView _ScrollViewContent;
        private ScrollView ScrollViewContent {
            get {
                if (_ScrollViewContent == null) {
                    _ScrollViewContent = new ScrollView();
                }
                return _ScrollViewContent;
            }
        }

        private ContentView _ContentViewImage;
        private ContentView ContentViewImage {
            get {
                if (_ContentViewImage == null) {
                    _ContentViewImage = new ContentView {
                        Margin = new Thickness(0, 0, 0, 30),
                    };
                }
                return _ContentViewImage;
            }
        }

        private Image _ImageLogo;
        private Image ImageLogo {
            get {
                if (_ImageLogo == null) {
                    _ImageLogo = new Image {
                        Aspect = Aspect.AspectFit,
                        Source = "splash_icon",
                        WidthRequest = 80,
                        HeightRequest = 80,
                    };
                }
                return _ImageLogo;
            }
            set {
                _ImageLogo = value;
            }
        }

        private Button _ButtonLogin;
        private Button ButtonLogin {
            get {
                if (_ButtonLogin == null) {
                    _ButtonLogin = new Button {
                        Text = "Log In",
                        FontSize = 18,
                        TextColor = Color.White,
                        BackgroundColor = Color.FromHex("618ec6"),
                        CornerRadius = 8,
                        HeightRequest = 50,
                        Margin = new Thickness(30, 30, 30, 0)

                    };
                    _ButtonLogin.Clicked += ButtonLogin_Clicked;
                    _ButtonLogin.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily);

                }
                return _ButtonLogin;
            }
        }

        private Button _ButtonClose;
        private Button ButtonClose {
            get {
                if (_ButtonClose == null) {
                    _ButtonClose = new Button {
                        Image = "nav_close",
                        Margin = new Thickness(20, 20, 0, 0),
                        BackgroundColor = Color.Transparent,
                        WidthRequest = 36,
                        HeightRequest = 36,
                        HorizontalOptions = LayoutOptions.Start,

                    };
                    _ButtonClose.Clicked += ButtonClose_Clicked;

                }
                return _ButtonClose;
            }
        }

        private Label _LabelForgotPassword;
        private Label LabelForgotPassword {
            get {
                if (_LabelForgotPassword == null) {
                    _LabelForgotPassword = new Label {
                        Text = "Forgot Password?",
                        HorizontalTextAlignment = TextAlignment.End,
                        FontSize = 16,
                        TextColor = Color.FromHex(AppTheme.SecondaryTextColor()),
                        BackgroundColor = Color.Transparent,
                        Margin = new Thickness(30, 10, 30, 0),
                        HorizontalOptions = LayoutOptions.End,
                    };
                    _LabelForgotPassword.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily);
                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += LabelForgotPassword_Tapped;
                    _LabelForgotPassword.GestureRecognizers.Add(tapGestureRecognizer);
                }

                return _LabelForgotPassword;
            }
        }

        private InputTextContentView _InputUsername;
        private InputTextContentView InputUsername {
            get {
                if (_InputUsername == null) {
                    _InputUsername = new InputTextContentView(InputTextIdentifierUsername, "Name", "Name", false, "input_username", this) {
                        Margin = new Thickness(30, 20, 30, 10)
                    };

                }
                return _InputUsername;
            }
        }

        private InputTextContentView _InputPassword;
        private InputTextContentView InputPassword {
            get {
                if (_InputPassword == null) {
                    _InputPassword = new InputTextContentView(InputTextIdentifierPassword, "Password", "Password", true, "input_password", this) {
                        Margin = new Thickness(30, 10, 30, 10)
                    };

                }
                return _InputPassword;
            }
        }

        private ContentView _ContentViewInstructionWrapper;
        private ContentView ContentViewInstructionWrapper {
            get {
                if (_ContentViewInstructionWrapper == null) {
                    _ContentViewInstructionWrapper = new ContentView {
                        BackgroundColor = Color.FromHex(AppTheme.SecondaryColor()),
                        Margin = new Thickness(30, 10, 30, 0),
                        IsVisible = false
                    };
                }
                return _ContentViewInstructionWrapper;
            }
        }
        private Label _LabelInstruction;
        private Label LabelInstruction {
            get {
                if (_LabelInstruction == null) {
                    _LabelInstruction = new Label {
                        Text = "Enter your email address and click return to start recovery",
                        HorizontalTextAlignment = TextAlignment.Start,
                        FontSize = 15,
                        TextColor = Color.FromHex(AppTheme.DefaultTextColor()),
                        BackgroundColor = Color.Transparent,
                        Margin = new Thickness(10, 10, 10, 10),
                        HorizontalOptions = LayoutOptions.End,
                    };
                    _LabelInstruction.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily);
                }

                return _LabelInstruction;
            }
        }

        private EntryBorderless _EntryEmailRecovery;
        public EntryBorderless EntryEmailRecovery {
            get {
                if (_EntryEmailRecovery == null) {
                    _EntryEmailRecovery = new EntryBorderless {
                        TextColor = Color.Black,
                        PlaceholderColor = Color.White.MultiplyAlpha(0.3),
                        HeightRequest = 50,
                        BackgroundColor = Color.White,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Margin = new Thickness(0, 0, 0, 0)
                    };
                    _EntryEmailRecovery.TextChanged += EntryEmailRecovery_TextChanged;
                    _EntryEmailRecovery.Completed += EntryEmailRecovery_Completed;

                }
                return _EntryEmailRecovery;
            }
        }

        private DimActivityIndicatorContentView _CustomActivityIndicator;
        private DimActivityIndicatorContentView CustomActivityIndicator {
            get {
                if (_CustomActivityIndicator == null) {
                    _CustomActivityIndicator = new DimActivityIndicatorContentView();
                }
                return _CustomActivityIndicator;
            }
        }

        #endregion

        #region Initialization
        public LoginContentPage() {
            this.Setup();
        }

        protected override void OnOrientationUpdate(DeviceOrientatione orientation) {
        }
        #endregion

        #region Private API
        private void Setup() {

            // image
            this.ContentViewImage.Content = this.ImageLogo;

            this.ContentViewInstructionWrapper.Content = new StackLayout {
                Children = {
                    this.LabelInstruction,
                    this.EntryEmailRecovery
                }
            };

            FlexLayout flexLayoutMainContent = new FlexLayout {
                Direction = FlexDirection.Column,
                JustifyContent = FlexJustify.Center,
                Children = {
                    this.ContentViewImage,
                    this.InputUsername,
                    this.InputPassword,
                    this.ButtonLogin,
                },
            };

            this.ScrollViewContent.Content = new StackLayout {
                Children = {
                    flexLayoutMainContent,
                    this.LabelForgotPassword,
                    this.ContentViewInstructionWrapper

                }
            };

            StackLayout stacklayout = new StackLayout {
                Children = {
                    this.ButtonClose,
                    this.ScrollViewContent
                }
            };

            RelativeLayout relativelayout = new RelativeLayout();

            // stack
            relativelayout.Children.Add(stacklayout, Constraint.Constant(0), Constraint.Constant(0),
            Constraint.RelativeToParent((parent) => {
                return parent.Width;
            }), Constraint.RelativeToParent((parent) => {
                return parent.Height;
            }));

            // loading
            relativelayout.Children.Add(CustomActivityIndicator, Constraint.Constant(0), Constraint.Constant(0),
            Constraint.RelativeToParent((parent) => {
                return parent.Width;
            }), Constraint.RelativeToParent((parent) => {
                return parent.Height;
            }));


            Content = relativelayout;
        }

        private async void ValidateLoginAsync() {
            //Application.Current.MainPage = new BaseNavigationPage(new ManageImageFileContentPage());
            if (!String.IsNullOrEmpty(this.TextUsername) && !String.IsNullOrEmpty(this.TextPassword)) {
                UserNetworkManager manager = new UserNetworkManager();

                this.CustomActivityIndicator.IsRunning = true;
                User user = await manager.LoginAsync(this.TextUsername, this.TextPassword);
                if (user.Success && String.IsNullOrEmpty(user.Message)) {
                    // save user login data to app data
                    Application.Current.Properties["User"] = ObjectSerializerHelper.ConvertObjectToBase64(user);
                    await Application.Current.SavePropertiesAsync();
                    Application.Current.MainPage = new BaseNavigationPage(new ManageImageFileContentPage(user));
                } else {
                    await DisplayAlert("Error", $"{user.Message}", "OK");
                }
                this.CustomActivityIndicator.IsRunning = false;

            } else {
                await DisplayAlert("Error", $"Username and password is required.", "OK");
            }



        }

        private async void RecoverEmailAsync() {
            if (!string.IsNullOrEmpty(this.TextEmail)) {
                UserNetworkManager manager = new UserNetworkManager();
                this.CustomActivityIndicator.IsRunning = true;
                bool success = await manager.SendEmailForRecovery(this.TextEmail.Trim());
                if (success) {
                    await DisplayAlert("Password Recovery", $"If an account exists with that email, you will receive an email within a few minutes", "OK");
                } else {
                    await DisplayAlert("Network Error", $"There was an issue with the network. Please try again later", "OK");
                }
                this.CustomActivityIndicator.IsRunning = false;

            } else {
                await DisplayAlert("Error", $"Please fill out the form before submitting", "OK");
            }
        }

        #region EventHandlers
        // Buttons
        void ButtonLogin_Clicked(object sender, EventArgs e) {
            this.ValidateLoginAsync();
        }

        void ButtonClose_Clicked(object sender, EventArgs e) {
            this.Navigation.PopModalAsync(true);
        }

        // GestureRecognizers
        void LabelForgotPassword_Tapped(object sender, EventArgs e) {
            this.ContentViewInstructionWrapper.IsVisible = true;
            this.EntryEmailRecovery.Focus();
        }

        void EntryEmailRecovery_TextChanged(object sender, EventArgs e) {
            Entry entry = (Entry)sender;
            this.TextEmail = entry.Text;
        }
        void EntryEmailRecovery_Completed(object sender, EventArgs e) {
            Entry entry = (Entry)sender;
            this.RecoverEmailAsync();
        }

        #endregion

        #endregion

        #region Public API

        #endregion

        #region Delegates

        void IInputTextDelegate.Input_TextChanged(string text, InputTextContentView inputText) {
            if (inputText.Identifier == InputTextIdentifierUsername) {
                this.TextUsername = text;
            } else {
                this.TextPassword = text;
            }
        }

        void IInputTextDelegate.Input_DidPressReturn(string text, InputTextContentView inputText) {
            if (inputText.Identifier == InputTextIdentifierUsername) {
                this.InputPassword.EntryItem.Focus();
            } else {
                this.ValidateLoginAsync();
            }
        }
        #endregion
    }
}
