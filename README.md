# GSPro_Focus_Reset
GSPro Focus Reset

There might be a problem that your GSPro App windows lost his focus when clicked somewhere outside with the mouse, e.g. in the Skytrak API. In this case, the keyboard shortcuts and applications such as Unified Remote / Air Keyboard will no longer work. I wrote a small app "GSPro_Focus_Reset.exe" for this purpose. Once this has started, it is checked every 3 seconds whether GSPRO has the focus and, if not so, set it.

If something has to be written in another app or done apart from mouse clicks, the tool can be temporarily deactivated using the STOP button, otherwise it would set the focus back to GSPro after 3 seconds ;-)

Since Version 1.011 you can use this tool with all other apps, that need a foreground focus and save interval and appname in its config file.
New Version 1.014:
You can either keep the app in the foreground or set the focus on the window to be monitored

![Screenshot 2023-02-24 182210](https://user-images.githubusercontent.com/58996871/221250067-3b93b50b-7bda-4963-a0a6-3e37862dcce7.png)
