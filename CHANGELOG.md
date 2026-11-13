# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).


## [2.1.1] - 2026-11-12

### Fixed

- Load on R.E.P.O. 0.5.1. `ShopPopulateItemVolumes` gained a second overload, and the untyped patch threw `AmbiguousMatchException` during `Awake`, so health regen never attached.
- Regen postfix now targets `LevelGenerator.GenerateDone` instead of `PlayerController`.
- Level and host checks use `SemiFunc.RunIsLevel` and `SemiFunc.IsMasterClientOrSingleplayer`. `levelShop` is a list in 0.5.1.
- A single failed patch is logged and no longer stops the other patches from loading.

## [Unreleased]

### Add

- Heals on the server level
- Apply the same healing to everyone; now only applies healing on item destroy.

## [1.0.1] - 2025-03-04

### Updated

- Emphasis on the `HealAmountMultiplier` in README.

## [1.0.0] - 2025-03-04

### Added

- Health Packs now work for the whole team.
