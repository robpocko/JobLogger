Getting to work with Windows UI Library
=======================================

Add NuGet package Microsoft.UI.Xaml to UWP project (JobLogger)

After it installed, got the following message;

		Thanks for installing the WinUI nuget package! Don't forget to add this to your app.xaml:

			<Application.Resources>
				<XamlControlsResources xmlns="using:Microsoft.UI.Xaml.Controls"/>
			</Application.Resources>

		See http://aka.ms/winui for more information.

Note that I followed the instructions from here as well: https://docs.microsoft.com/en-us/uwp/toolkits/winui/getting-started

So my app.xaml ended looking like

    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <XamlControlsResources xmlns="using:Microsoft.UI.Xaml.Controls"/>
                <ResourceDictionary Source="/Styles/Styles.xaml"/>
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>

	i.e. added "<XamlControlsResources xmlns="using:Microsoft.UI.Xaml.Controls"/>"

Also had to increase target version to 17763
