﻿using System;
using EVSlideShow.Core.Common;
using EVSlideShow.Core.Components.CustomRenderers;
using EVSlideShow.Core.Constants;
using EVSlideShow.Core.ViewModels;
using EVSlideShow.Core.ViewModels.Base;
using EVSlideShow.Core.Views.Base;
using EVSlideShow.Core.Views.ContentViews;
using Xamarin.Forms;

namespace EVSlideShow.Core.Views {
    public class SignUpContentPage : BaseContentPage<SignUpViewModel>, IInputTitle {

        #region Variables
        private const string InputTextIdentifierEVType = "evtype";
        private const string InputTextIdentifierUsername = "username";
        private const string InputTextIdentifierPassword = "password";
        private const string InputTextIdentifierEmail = "email";
        private const string InputTextIdentifierEmailRepeat = "emailrepeat";

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

        private FlexLayout _FlexLayoutContent;
        private FlexLayout FlexLayoutContent {
            get {
                if (_FlexLayoutContent == null) {
                    _FlexLayoutContent = new FlexLayout {
                        Direction = FlexDirection.Column,
                        JustifyContent = FlexJustify.Center,
                    };
                }
                return _FlexLayoutContent;
            }
        }
        private ScrollView _ScrollViewContent;
        private ScrollView ScrollViewContent {
            get {
                if (_ScrollViewContent == null) {
                    _ScrollViewContent = new ScrollView();
                }
                return _ScrollViewContent;
            }
        }

        private StackLayout _StackLayout;
        private StackLayout StackLayout {
            get {
                if (_StackLayout == null) {
                    _StackLayout = new StackLayout();
                }
                return _StackLayout;
            }
        }

        private InputTextContentView _InputUsername;
        private InputTextContentView InputUsername {
            get {
                if (_InputUsername == null) {
                    _InputUsername = new InputTextContentView(InputTextIdentifierUsername, "Create Account Username", "Name", false, "input_username", this) {
                        Margin = new Thickness(30, 10, 30, 10),
                        TitleFontSize = 16
                    };

                }
                return _InputUsername;
            }
        }

        private InputTextContentView _InputEmail;
        private InputTextContentView InputEmail {
            get {
                if (_InputEmail == null) {
                    _InputEmail = new InputTextContentView(InputTextIdentifierEmail, "Account Email Address", "Email", false, "input_email", this) {
                        Margin = new Thickness(30, 10, 30, 10),
                        TitleFontSize = 16
                    };

                }
                return _InputEmail;
            }
        }
        private InputTextContentView _InputEmailRepeat;
        private InputTextContentView InputEmailRepeat {
            get {
                if (_InputEmailRepeat == null) {
                    _InputEmailRepeat = new InputTextContentView(InputTextIdentifierEmailRepeat, "Confirm Email Address", "Confirm Email", false, "input_email", this) {
                        Margin = new Thickness(30, 10, 30, 10),
                        TitleFontSize = 16

                    };

                }
                return _InputEmailRepeat;
            }
        }
        private InputTextContentView _InputPassword;
        private InputTextContentView InputPassword {
            get {
                if (_InputPassword == null) {
                    _InputPassword = new InputTextContentView(InputTextIdentifierPassword, "Password (Only use A-z, 0-9)", "Password", true, "input_password", this) {
                        Margin = new Thickness(30, 10, 30, 10),
                        TitleFontSize = 16
                    };

                }
                return _InputPassword;
            }
        }

        private Button _ButtonSignUp;
        private Button ButtonSignUp {
            get {
                if (_ButtonSignUp == null) {
                    _ButtonSignUp = new Button {
                        Text = "Sign Up",
                        FontSize = 18,
                        TextColor = Color.White,
                        BackgroundColor = Color.FromHex("618ec6"),
                        CornerRadius = 8,
                        HeightRequest = 50,
                        Margin = new Thickness(30, 30, 30, 0)

                    };
                    _ButtonSignUp.Clicked += ButtonSignUp_Clicked;
                    _ButtonSignUp.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily);

                }
                return _ButtonSignUp;
            }
        }

        private Label _LabelViewTermsOfUse;
        private Label LabelViewTermsOfUse {
            get {
                if (_LabelViewTermsOfUse == null) {
                    _LabelViewTermsOfUse = new Label {
                        Text = "View Terms of Use",
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 16,
                        TextColor = Color.White,
                        BackgroundColor = Color.Transparent,
                        Margin = new Thickness(30, 10, 30, 30),
                        HorizontalOptions = LayoutOptions.End,
                    };
                    _LabelViewTermsOfUse.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily);
                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += LabelViewTermsOfUse_Tapped;
                    _LabelViewTermsOfUse.GestureRecognizers.Add(tapGestureRecognizer);
                }

                return _LabelViewTermsOfUse;
            }
        }
        #endregion

        #region Initialization
        public SignUpContentPage() {
            this.Setup();
        }

        protected override void OnOrientationUpdate(DeviceOrientatione orientation) {
        }
        #endregion

        #region Private API
        private void Setup() {

            // stacklayout
            this.StackLayout.Children.Add(this.ButtonClose);
            this.StackLayout.Children.Add(this.ScrollViewContent);

            // image
            this.ContentViewImage.Content = this.ImageLogo;

            // scrollview
            this.ScrollViewContent.Content = this.FlexLayoutContent;

            // flexlayout
            this.FlexLayoutContent.Children.Add(this.ContentViewImage);
            this.FlexLayoutContent.Children.Add(this.InputUsername);
            this.FlexLayoutContent.Children.Add(this.InputEmail);
            this.FlexLayoutContent.Children.Add(this.InputEmailRepeat);
            this.FlexLayoutContent.Children.Add(this.InputPassword);
            this.FlexLayoutContent.Children.Add(this.ButtonSignUp);
            this.FlexLayoutContent.Children.Add(this.LabelViewTermsOfUse);

            Content = this.StackLayout;

        }

        private void RegisterAccount() {
            DisplayAlert("Register Account", $"Registered account {this.ViewModel.Username}", "OK");

        }
        #region EventHandlers
        // Buttons
        void ButtonClose_Clicked(object sender, EventArgs e) {
            this.Navigation.PopModalAsync(true);
        }
        void ButtonSignUp_Clicked(object sender, EventArgs e) {
            this.RegisterAccount();
        }
        // GestureRecognizers
        void LabelViewTermsOfUse_Tapped(object sender, EventArgs e) {
            DisplayAlert("Terms of Use", $"Display a web view to terms of use", "OK");
        }

        #endregion

        #endregion

        #region Public API

        #endregion

        #region Delegates
        void IInputTitle.Input_TextChanged(string text, InputTextContentView inputText) {
            switch (inputText.Identifier) {
                case InputTextIdentifierUsername:
                    this.ViewModel.Username = text;
                    break;
                case InputTextIdentifierEmail:
                    this.ViewModel.Email = text;
                    break;
                case InputTextIdentifierEmailRepeat:
                    this.ViewModel.EmailRepeat = text;
                    break;
                case InputTextIdentifierPassword:
                    this.ViewModel.Password = text;
                    break;
            }
        }

        void IInputTitle.Input_DidPressReturn(string text, InputTextContentView inputText) {
            switch (inputText.Identifier) {
                case InputTextIdentifierUsername:
                    this.InputEmail.EntryItem.Focus();
                    break;
                case InputTextIdentifierEmail:
                    this.InputEmailRepeat.EntryItem.Focus();
                    break;
                case InputTextIdentifierEmailRepeat:
                    this.InputPassword.EntryItem.Focus();
                    break;
                case InputTextIdentifierPassword:
                    this.RegisterAccount();
                    break;
            }
        }
        #endregion
    }
}
