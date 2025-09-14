# Changelog

All notable changes to this project will be documented in this file.

A changelog wasn't kept until version 0.7.0, so the changelog is currently being filled in.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

- The \[HideIcon\] attribute, which automatically turns off the scene-view icon for a component. 

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
