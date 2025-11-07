# WiX Installer Styling Guide

This guide explains how to customize the visual appearance of the Aemulus XR Reporting installer.

## Custom Images

The WiX installer can use custom bitmap images to brand your installer. The images are referenced in `AemulusXRReporting.wxs`.

### Required Image Specifications

#### 1. Banner Image (bannrbmp.bmp)
- **Dimensions**: 493 × 58 pixels
- **Format**: 24-bit BMP file
- **Usage**: Displayed at the top of interior dialogs (License, Feature Selection, Install Location, Progress)
- **Design Tips**:
  - Use your logo on the left side
  - Keep the design simple and horizontal
  - Use brand colors
  - Ensure good contrast with text that appears below it

#### 2. Dialog Background Image (dlgbmp.bmp)
- **Dimensions**: 493 × 312 pixels
- **Format**: 24-bit BMP file
- **Usage**: Displayed on the left side of Welcome and Finish/Exit dialogs
- **Design Tips**:
  - This is the first and last thing users see
  - Can include product logo, tagline, or branded graphics
  - Should be visually appealing but not too busy
  - Vertical orientation works well

### How to Add Custom Images

1. **Create your images** with the exact dimensions specified above
2. **Save as BMP files**:
   - `banner.bmp` (493×58)
   - `dialog.bmp` (493×312)
3. **Place files** in `src/installer/` directory
4. **Uncomment the lines** in `AemulusXRReporting.wxs`:
   ```xml
   <WixVariable Id="WixUIBannerBmp" Value="banner.bmp" />
   <WixVariable Id="WixUIDialogBmp" Value="dialog.bmp" />
   ```
5. **Rebuild** the installer using `build_and_package.ps1`

## Image Creation Tools

### Recommended Tools:
- **GIMP** (Free): Create/edit BMP files, resize images
- **Paint.NET** (Free): Simple BMP editing
- **Adobe Photoshop**: Professional option
- **Figma/Canva**: Design then export as PNG, convert to BMP

### Converting PNG to BMP:
If you have PNG files, you can convert them:

**Using PowerShell:**
```powershell
Add-Type -AssemblyName System.Drawing
$png = [System.Drawing.Image]::FromFile("input.png")
$png.Save("output.bmp", [System.Drawing.Imaging.ImageFormat]::Bmp)
$png.Dispose()
```

**Using GIMP:**
1. File → Open (select PNG)
2. Image → Mode → RGB (if not already)
3. File → Export As
4. Choose .bmp extension
5. Select "24-bit" in export options

## Design Recommendations

### Banner (493×58):
```
┌─────────────────────────────────────────────────┐
│ [Logo] Aemulus XR Reporting                    │
│ Installer                                       │
└─────────────────────────────────────────────────┘
```

### Dialog (493×312):
```
┌──────────────────┐
│                  │
│   [Product Logo] │
│                  │
│   Aemulus XR     │
│   Reporting      │
│                  │
│   [Tagline or    │
│    visual        │
│    branding]     │
│                  │
└──────────────────┘
```

### Color Palette Suggestions:
- Use your existing brand colors
- Based on your logo (Aemulus_LogoSymbol_Blue1024.png):
  - Primary: Aemulus Blue
  - Accent: White or light gray
  - Background: Gradient or solid color

### Best Practices:
1. **Consistency**: Match your application's visual identity
2. **Readability**: Ensure any text is crisp and easy to read
3. **File Size**: Keep BMPs reasonably sized (under 1MB each)
4. **Test**: Always test the installer after adding images
5. **Fallback**: If images aren't provided, WiX uses default blue/white theme

## Current Installer Styling

### What's Already Customized:
- ✅ Application icon (aemulus.ico) used for shortcuts and Add/Remove Programs
- ✅ License file (license.rtf) with MIT License
- ✅ Multi-page dialog flow with WixUI_FeatureTree
- ✅ Custom install directory selection
- ✅ Feature selection (Desktop Shortcut)
- ✅ Launch application checkbox on finish

### What You Can Add:
- ⬜ Banner bitmap (493×58)
- ⬜ Dialog bitmap (493×312)
- ⬜ Custom fonts/text colors (requires more advanced customization)
- ⬜ Fully custom dialog layouts (requires creating custom WXS dialog files)

## Advanced Styling

For more advanced customization beyond images, you would need to:

1. **Create Custom Dialogs**: Copy WiX UI extension source files and modify
2. **Custom Themes**: Requires building custom WXL localization files
3. **Font/Color Changes**: Requires modifying dialog XML definitions

These are significantly more complex and require deep WiX knowledge.

## Example: Quick Start

Want to get started quickly? Here's a simple approach:

1. Find your existing logo/branding images
2. Use an online tool like [photopea.com](https://www.photopea.com/) (free Photoshop alternative)
3. Create new image: 493×58 for banner
4. Add your logo and text
5. Export as PNG, then convert to BMP
6. Repeat for 493×312 dialog image
7. Place in `src/installer/`
8. Uncomment the WixVariable lines
9. Run `build_and_package.ps1`

## Resources

- [WiX Toolset Documentation - WixUI](https://wixtoolset.org/docs/tools/wixext/wixui/)
- [WiX Tutorial - Customizing UI](https://www.firegiant.com/wix/tutorial/user-interface/)
- Your existing logo: `src/Resources/Aemulus_LogoSymbol_Blue1024.png`

## Need Help?

If you need assistance creating the images:
1. Provide your brand colors and logo
2. Specify any text/tagline to include
3. We can create templates or provide sample code to generate them

---

**Note**: The installer will work perfectly without custom images - they're purely optional branding elements.
