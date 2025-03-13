# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

### Changed

## [1.3.0] - 2025-03-13

### Added
- Added filter by log level on log panel.
- Added filter by log message on log panel.

### Changed
- Pass the Log object to `LogHander.Emit` function. NOTE: will break any panels that override `Log`.

## [1.2.0] - 2025-01-10

### Added

- Added MOTD.
- Added Info command.
- Added ActiveChanged event.
- Added settings for console margins.
- Added settings for console size.
- Added SetConsoleSize command.

### Changed

- Changed level of dev console initialized message from info to debug.

## [1.1.0] - 2024-11-13

### Added

- Added CHANGELOG.md.

### Changed

- Changed terminal panel to get its default prompt value from project settings `addons/godotdevconsole/terminal_panel/prompt`.
- Changed terminal panel to get its default show logs value from project settings `addons/godotdevconsole/terminal_panel/show_logs`.

## [1.0.0] - 2024-11-01

### Added

- Added Godot dev console plugin.
- Added LICENSE.
- Added README.md.

[unreleased]: https://github.com/Logtism/godotdevconsole/compare/v1.3.0...HEAD
[1.3.0]: https://github.com/Logtism/godotdevconsole/compare/v1.2.0...v1.3.0
[1.2.0]: https://github.com/Logtism/godotdevconsole/compare/v1.1.0...v1.2.0
[1.1.0]: https://github.com/Logtism/godotdevconsole/compare/v1.0.0...v1.1.0
[1.0.0]: https://github.com/Logtism/godotdevconsole/releases/tag/v1.0.0
