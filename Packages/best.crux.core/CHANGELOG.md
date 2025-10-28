# Changelog

All notable changes to this project will be documented in this file.

A changelog wasn't kept until version 0.7.0, so the changelog is currently being filled in.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## Unreleased

### Added

- A dev-mode-only context-menu option to "prepare" a package
  - Right-click a PackageManifest asset and select "Prepare"
  - This will perform the following steps:
    - Reserialize the sample templates directory
    - Reserialize the package
    - Delete Samples~
    - Copy the Sample Templates directory to Samples~
  - This gets the package ready for a release.

### Fixed

- BeginEnumRevealArea couldn't accept multiple enum values for non-Flags enums
- Reveal areas added via script no longer flicker for one frame
  - Areas placed in UI documents still flicker
- Corrected many cases where gadgets would not get drawn correctly
  - For example, they didn't work for inherited fields or properties
- GadgetPropertyDrawer now correctly draws a foldout

### Removed

- The DrawGadgets attribute
  - Classes that need gadgets should just create a custom property drawer
  - Having to include the attribute everywhere didn't make sense

## [0.8.2] - 2025-10-23

### Added

- BeginRevealArea and EndRevealArea attributes
  - These allow you to create RevealAreas without needing to create a custom property drawer
- The BeginEnumRevealArea attribute
- The DrawGadgets attribute, which lets these attributes work without needing a custom property drawer
  - You can also derive from the GadgetPropertyDrawer and tell it to draw your class, if needed
- CoreLog, for dev-mode-only logging
- Extension methods for finding attributes on fields or properties referenced by a SerializedProperty
  - These are public, since other packages may need to do the same thing.

### Changed

- DecoratedList<T> can now return List<T>.Enumerator from its GetEnumerator method
  - This is very nitpicky, but this prevents some unnecessary memory allocation
- Moved CreatePropertyFields from UpgradablePropertyDrawer and into GadgetPropertyDrawer
  - This is a breaking change, but nothing actually depends on this yet.

### Fixed

- RevealArea now works correctly when constructed directly
  - Setup code used to only run in its UxmlFactory.
- EnumRevealArea has been fixed in the same way

## [0.8.1] - 2025-10-14

### Added

- A tool to reserialize all package assets
  - This prevents random version-control churn when serialized data gets upgraded.
  - It also tidies up unused data.
- An "Origin" prefab, used for constraints that ignore the player's own transform

### Changed

- Renamed the Assembly Definition assets
  - This would be breaking for anyone referencing them by name, rather than by GUID
  - ...but you wouldn't do that, would you? (; 

## [0.8.0] - 2025-09-15

### Changed

- **BREAKING:** Move all demo assets into package samples.
  - This included renaming some assemblies
  - This is a breaking change because someone *could*, theoretically, be relying on these assets
- Move the CopyGuid class into a new Developer assembly
  - This assembly is only compiled when CRUX_DEV is defined
- Remove all hardcoded asset paths
  - These have been replaced by GUID+FileID pairs

### Fixed

- The manifest's changelog URL was incorrect
- CopyGuid would log an error if no object was selected

## [0.7.1] - 2025-09-15

### Added

- The \[HideIcon\] attribute, which automatically turns off the scene-view icon for a component. 
- Documentation and changelog links in the package manifest

### Changed

- The project now properly displays the MIT license.

## [0.7.0] - 2025-09-07

### Changed

- **BREAKING:** UpgradablePropertyDrawerAttribute now uses an AssetRef, not a file path.

## [0.6.4] - 2025-07-21

### Added

- EnumRevealArea, which can reveal parts of an editor depending on the value of an enum property

## [0.6.3] - 2025-04-10

### Added

- More entries in the CoreConsts class

### Changed

- Updated alert-box styling

## [0.6.2] - 2025-04-08

### Fixed

- DecoratedLists displayed an incorrect label

## [0.6.1] - 2025-04-08

### Added

- DocRefInert and TooltipRefInert, which can be used by custom editors, but aren't automatically used to insert buttons.
  - This is important for the DecoratedList class
 
## [0.6.0] - 2025-04-08

### Added

- DecoratedList, which allows things like tooltip buttons to be applied to an entire list (rather than its individual elements)
  - Unity 6.0 introduced PropertyAttribute.applyToCollection, but that's not available in 2022.3.22f1

### Changed

- **BREAKING:** Custom attributes were moved into a new namespace

## [0.5.0] - 2025-04-07

### Changed

- **BREAKING:** Namespaces were renamed

## [0.4.3] - 2025-04-01

### Fixed

- RevealArea had an incorrectly set background color

## [0.4.2] - 2025-04-01

### Fixed

- CreatePropertyGUI was being called recursively when drawing upgradable data
  - This was causing the wrong property drawer to try to draw the data
  - Instead, a new PropertyField is created and drawn
- A stylesheet reference was missing in the Upgradable UXML document
