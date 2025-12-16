# Description
This is a fork of the popular [repo.mods](https://github.com/EvilCheetah/repo.mods) created by [EvilCheetah](https://github.com/EvilCheetah/repo.mods) including the widely used mod [TeamUpgrades
](https://thunderstore.io/c/repo/p/EvilCheetah/TeamUpgrades/) updating them to the recent R.E.P.O. v0.3.0 version and fixing bugs. 

# Features & Changes
## BetterTeamUpgrades
- This fork currently fixes issues with the original [TeamUpgrades](https://thunderstore.io/c/repo/p/EvilCheetah/TeamUpgrades/) mod causing it to break.
- This fork updates the original out-of-date [TeamUpgrades](https://thunderstore.io/c/repo/p/EvilCheetah/TeamUpgrades/) mod to support the recent upgrade additions including:
    - Player Health Upgrade
    - Player Grab Strength Upgrade
    - Player Throw Strength Upgrade
    - Player Range Upgrade
    - Player Sprint Upgrade
    - Player Stamina Upgrade
    - Player Crouch Rest Upgrade
    - Player Extra Jump Upgrade
    - Player Tumble Launch Upgrade
    - Player Tumble Wings Upgrade
    - Player Tumble Climb Upgrade
    - Death Head Batter Upgrade
    - Map Player Count Upgrade
 
# Future Plans
- Implement a native late joining system that supports upgrade syncing like the currently broken `LateJoinSharedUpgradesByNastyPablo` mod directly integrated within the [BetterTeamUpgrades](https://thunderstore.io/c/repo/p/MrBytesized/BetterTeamUpgrades/) mod.
- Fix the crashes and bugs caused due to the recent v0.3.0 R.E.P.O. update making the other [repo.mods](https://github.com/EvilCheetah/repo.mods) to no longer work: [ScalingPrices](https://thunderstore.io/c/repo/p/EvilCheetah/ScalingPrices/), [BetterTeamUpgrades](https://thunderstore.io/c/repo/p/MrBytesized/BetterTeamUpgrades/) and [KurwaHunter](https://thunderstore.io/c/repo/p/EvilCheetah/KurwaHunter/).
- I plan on overhauling the current [TeamUpgrades](https://thunderstore.io/c/repo/p/EvilCheetah/TeamUpgrades/) system or updating the [SharedUpgrades](https://thunderstore.io/c/repo/p/Traktool/SharedUpgrades/) mod to fix the _known issue_ which causes the upgrade to not be applied until level change as the implementation of that mod is probably a better and more flexible approach allowing modded upgrades as well but for now i just wanted a fully working vanilla mod for now so enjoy!
- I plan on reimplementing the health glitch as a mod because i miss it XD

# Project Structure:
This repo holds multiple projects, each in its own branch. Below is an overview:

| Branch                      | Description                                                                                        | Link                                                                                            | Changed/Updated? |
|-----------------------------|----------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------|------------------|
| `master` (this branch)      | Project Entrypoint                                                                                 | [View on GitHub](https://github.com/W1ll-Gale/better.repo.mods/tree/master)                     | ✅                |
| `BetterTeamUpgrades (moved to new repo)` | [BetterTeamUpgrades](https://thunderstore.io/c/repo/p/MrBytesized/BetterTeamUpgrades/) Mod Project | [View on GitHub](https://github.com/W1ll-Gale/REPO.BetterTeamUpgrades)  | ✅                |
| `mods.scaling-prices`       | [ScalingPrices](https://thunderstore.io/c/repo/p/EvilCheetah/ScalingPrices/) Mod Project           | [View on GitHub](https://github.com/W1ll-Gale/better.repo.mods/tree/mods.better-scaling-prices) | ❌                |
| `mods.better-team-heals`    | [BetterHeals](https://thunderstore.io/c/repo/p/EvilCheetah/TeamHeals/) Mod Project             | [View on GitHub](https://github.com/W1ll-Gale/better.repo.mods/tree/mods.better-team-heals)     | ✅                |
| `mods.kurwa-hunter`         | [KurwaHunter](https://thunderstore.io/c/repo/p/EvilCheetah/KurwaHunter/) Mod Project               | [View on GitHub](https://github.com/W1ll-Gale/better.repo.mods/tree/mods.better-kurwa-hunter)   | ❌                |


## Feedback and Suggestions
If you have any feedback or suggestions, feel free to open an issue on the [GitHub repository](https://github.com/W1ll-Gale/better.repo.mods).
