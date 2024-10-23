# SRT Editor

**SRT Editor** is a versatile tool designed to help you manage `.srt` subtitle files for movies and TV shows. It provides two main functionalities: a **Time Editor** to adjust subtitle timings and a **Name Editor** to rename subtitle files based on the associated video files.

## Features

### 1. Time Editor
The **Time Editor** allows you to adjust the timing of subtitle files in two ways:

- **Delay**: Shift all subtitle timings forward or backward by a specified amount of time. This is helpful if the subtitles are slightly out of sync with the video.
  
  **Example**:  
  Shift subtitles by `+3 seconds` to sync them with delayed audio.
  
- **Time Scale Difference**: If your subtitles drift out of sync over time, the Scale feature adjusts the timing proportionally over the entire video length.

  **Example**:  
  You can correct subtitles that start in sync but progressively fall behind or run ahead by scaling them to match the video's duration.

### 2. Name Editor
The **Name Editor** helps you automatically rename subtitle files to match the corresponding video files in a folder. You can choose from two renaming options:

- **Option 1: Copy Video File Names**  
  This option renames subtitle files to match the names of the corresponding video files in the folder. If the `.srt` and video files are in the same folder but don’t share the same name, this will fix that.

  **Example**:  
  Rename `subtitle.srt` to `movie.mp4` → `movie.srt`.
  
- **Option 2: Extend Subtitle File Names**  
  Add a custom string to the existing subtitle filenames. This is useful when you want to append specific details like a language code or version.

  **Example**:  
  Rename `subtitle.srt` to `subtitle_English.srt`.

- **Combine Options 1 and 2**: You can use both options together to rename `.srt` files based on video filenames while also appending custom text.

  **Example**:  
  Rename `subtitle.srt` to `movie_English.srt`.

## How to Use

### Time Editor
1. **Delay**:  
   - Enter the number of seconds to shift the subtitle timings (positive for forward, negative for backward).
2. **Time Scale Difference**:  
   - Input the desired start and end times for the subtitle file to sync perfectly with the video.

### Name Editor
1. **Copy Names from Video Files**:  
   - Ensure both video and subtitle files are in the same folder. The tool will match and rename the `.srt` files to have the same name as the video files.
2. **Extend Existing Names**:  
   - Enter a custom string to append to all subtitle filenames.
3. **Combine Both**:  
   - Rename subtitles to match video filenames and append additional text in one operation.

## Example Workflow

1. **Synchronizing Subtitles**:  
   You notice that the subtitles are delayed by 2 seconds throughout the video. You can open the Time Editor, input `-2 seconds`, and shift the subtitle timings backward.

2. **Renaming Subtitles**:  
   In a folder with `.mp4` and `.srt` files, the subtitle names don’t match the video names. Using the Name Editor, you can quickly rename all subtitle files to match their respective video files.

3. **Appending Text to Subtitles**:  
   After renaming subtitles to match video filenames, you want to append "_English" to all `.srt` files. Use the "Extend Existing Names" option to achieve this.

