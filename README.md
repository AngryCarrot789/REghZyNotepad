# REghZyNotepad
A simple replica of windows notepad but with a dark theme and more features (MVVM i think)

Preview of the dark theme (there's a light theme too)

![](preview.png)

This is basically notepad, only without the finding feature (not yet atleast) but extra useful features like:
- CTRL + C to copy an entire line (if you haven't selected any text)
- CTRL + X to cut an entire line (if you haven't selected any text)
- CTRL + Shift + A to select an entire line
- Horizontal scrolling when holding shift
- Zoom/change font size when holding CTRL and scrolling (slightly buggy if you're not at the bottom of the text editor)
- and more

Also this is (i think lol) the first time i made a proper "mvvm" progra, so it might not be 100% what some would call "standard"... but eh. it works, and there's no view dependent stuff sort of :)

# to download
this uses a submodule of another repo i used for utils and stuff. so i think you do (in cmd or whatever):
`git clone --recursive https://github.com/AngryCarrot789/DragonJetzNotepad`
and then maybe `git submodule --init --recursive` probbaly... i havent done that before but ive seen others do it
