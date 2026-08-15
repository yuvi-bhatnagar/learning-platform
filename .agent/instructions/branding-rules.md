# Branding and Logo Usage Rules

This instruction governs how the existing logo assets should be used across layouts, headers, sidebars, and authentication screens.

## Existing Assets

All brand logo assets are located in the `wwwroot/images/brand/` directory:
- `learning-path-logo.svg` (Primary logo)
- `learning-path-icon.svg` (Logo symbol/icon)
- `learning-path-icon-dark.svg` (Icon variant for dark backgrounds)
- `learning-path-wordmark.svg` (Standalone wordmark text)

*Note: For the application's browser tab favicon, standard mapping is configured to use the logo icon.*

## Usage Guidelines

### 1. Primary Logo (`learning-path-logo.svg`)
- **Use for**: Main website header branding on light backgrounds, public-facing landing portals, registration or sign-in screens, and expanded sidebar headers.
- **Visual Context**: Ensure surrounding components provide sufficient background contrast.

### 2. Icon (`learning-path-icon.svg`)
- **Use for**: Compact sidebar layouts, collapsed navigation views, small square profiles, or mobile layouts where the full logo text does not fit.

### 3. Dark Icon (`learning-path-icon-dark.svg`)
- **Use for**: Collapsed sidebars or buttons that have a dark background.

### 4. Wordmark (`learning-path-wordmark.svg`)
- **Use for**: Text-focused footers or header locations where the standalone symbol is not required.

---

## Technical Constraints

- **No Modifications**: Never recreate, redraw, rename, crop, or modify these SVG assets.
- **Reference Paths**: Always reference the existing files from the `/images/brand/` directory.
- **Image Tags**: Prefer native Razor image references such as:
  ```html
  <img src="~/images/brand/learning-path-logo.svg" alt="Learning Path Logo" ... />
  ```
- **Aspect Ratio**: Always preserve the original logo proportions. Do not stretch, rotate, apply drop shadows, or add borders directly to the image elements.
- **Spacing**: Keep adequate padding/margin around logos to maintain clear readability.
