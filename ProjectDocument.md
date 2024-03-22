# Sling! #

## Summary ##

_Sling Fighter!_ is a slingshot-style arcade game where you launch into a dummy at full force! Compete for the best score in this no-holds-barred action brawl. This game contains 15 waves and a local leaderboard system.

## Project Resources

[Itch page](https://slingfighter.itch.io/sling-fighter)
[Trailor](https://youtu.be/LsbTWjYsmSs)  
[Press Kit]([https://dopresskit.com/](https://slingfighter.itch.io/sling-fighter])  
[Proposal: make your own copy of the linked doc.]([https://docs.google.com/document/d/1qwWCpMwKJGOLQ-rRJt8G8zisCa2XHFhv6zSWars0eWM/edit?usp=sharing](https://docs.google.com/document/d/1C83mvbO1hN6x2nOqhr4wfracJC8zQ0X5KEP7wMynV6c/edit?usp=sharing])  

## Gameplay Explanation ##

This game was designed for controller, but it will also work for keyboard and mouse. Use 'A' and the 'left stick' to pull back on the ropes, and let go to fire onto enemies. 'LB' and 'RB' are your sidesteps.

Read more on itch!

# Main Roles #

## Producer - Lucas Wang

As the producer, my primary responsibility was to manage the direction of our game and make sure that everyone understands their roles. It was important for me to avoid taking over the project and making every decision. Instead, I saw it as my responsibility to facilitate a reliable communication channel within our group and serve as a knowledgeable source of information about our project.

In preparation for this project, I carefully read over the [GameProject.md](https://github.com/dr-jam/GameplayProgramming/blob/master/GameProject.md) document provided to us by the professor. I wanted to ensure that we did not miss out on important aspects of the project and end up having to rush them. I talked to past students who had taken this class and looked over their `GameProject.md` documents linked in the [Project Groups Spreadsheet](https://docs.google.com/spreadsheets/d/1OUz9atsn2HAFm9Wa97dMX5Q5Pg5Whuir-C6jLmuWleM/edit?usp=sharing). I jotted down important tasks and made sure that broad deadlines were covered. For example, I noted that the Gameplay Tester role needed to collect at least 10 pieces of feedback and the Press Kit & Trailer role could be done on itch.io (with a video published on YouTube). I made these decisions early so that our team had a clear direction and idea of what our timeline would look like.

I created a broad timeline overview for our [Initial Plan](https://docs.google.com/document/d/1C83mvbO1hN6x2nOqhr4wfracJC8zQ0X5KEP7wMynV6c/edit?usp=sharing) and later iterated on it to produce a [Gantt chart](https://drive.google.com/file/d/1Wmc_jXUsYLCNVoHWeGYUSb-fZbAOdSkT/view?usp=sharing) after receiving feedback from the professor. Some examples of other work that I did as Producer:

- _Meeting Notes_ - For this project, our team conducted a total of 7 meetings, 5 of which were formally documented. For each meeting, I prepared a document that contained Updates, (Meeting) Agenda, Tasks, and a Postmortem section. I did this for two reasons: 1) so that each meeting would be efficient and cover all necessary topics, and 2) so that team members who missed the meeting could read them to catch up. A folder of my meeting notes can be found [here](https://drive.google.com/drive/folders/1iQh3dOM1LTEoZRAZfyugQAw6IgIBF4-z?usp=sharing).
- _Playtests + Playtest Notes_ - Our team conducted several formal playtests throughout this project. We did an in-person close play of games that were similar in concept to our initial game idea (see [Close Play Notes](https://docs.google.com/document/d/1scHNr-ufXpqL_Jk3Jl3NwZVNuqxHBwZrDwwwx5BAPwI/edit?usp=sharing)). We did an alpha, beta, and full game loop playtest of our builds and took notes. A folder containing the documents that I designed for this purpose can be found [here](https://drive.google.com/drive/folders/1uxBcECSqo5QJZQRb4cv_ltPRpya6PuLF?usp=sharing).
- _Meeting Miscellaneous_ - For in-person meetings and playtests, I booked meeting rooms and borrowed Xbox controllers. I would usually opt for Cruess 256 or Cruess 1107 and book it through the Tool Room (Cruess 101). I borrowed Xbox Controllers from the Alt-Ctrl Lab and informed relevant parties. I was also in charge of returning them after meetings.
- _Designating workflow through Git_ - I knew that it was important for us to establish our GitHub workflow early, so I brought it up during an early meeting. I told my team to work in separate branches, and then @ me (in Discord) when they needed to merge. I would try to do this within a couple hours, or a day if this wasn't possible. After doing this project, I  realized that establishing branch-naming conventions would also be useful. I will remember this for future projects.
- _Designating workflow through Unity_ - As we progressed through the project, I realized that a majority of merge conflicts came from Scenes. To avoid work from being erased, I encouraged my team to save their work in prefabs. This would ensure that their work translated consistently across scenes, but also that I could replace their work efficiently.

- _Bug Fixing & Merge Conflicts_ - Quite a few bugs and merge conflicts emerged throughout our project. I solved most of these, including [the player model facing the wrong direction](https://github.com/rhythm0708/sling-fighter/blob/4538b1bf81eb0f0c06ded106905c0549548cde17/Assets/Scripts/Player/PlayerMovement.cs#L215), [music not stopping during the Continue scene](https://github.com/rhythm0708/sling-fighter/blob/c55e483a87fbad4349b597d06b550e2cdb5e026e/Assets/Scripts/Sound/SoundManager.cs#L99), and URP rendering issues amongst others. Jet was also very active in helping me fix bugs.

- _Writing and Documentation_ - I completed most of the writing for the Initial Plan and both Progress Reports, before letting my team edit and add observations. I did this to allow my team to focus on the project without having to worry about other tasks.
- _Coordinating Progress Reports_ - I coordinated progress report meetings with other teams and found times that also worked with my team. I also checked other teams' progress reports on us and caught errors; for example, one team omitted Daniel's name when writing down which members were present. I went back and corrected that.
- _Managing Communication and Organizational Procedures_ - Our team used Discord as our main channel of communication. On it, I posted updates and important information that I wanted to share with the team. I think that our team communicated well, as we were in constant communication and spoke up when we needed help. I also managed a [Google Drive folder](https://drive.google.com/drive/folders/1RleSwVFyKq-hQuMPw-QvtK3InfTamqMV?usp=drive_link) where team members could find all (external) resources that we used for this project.

### One final note

As Producer, I believe that it is important for every member of the team to be fairly credited for the work that they did. Our game has gone through an [alpha](https://github.com/rhythm0708/sling-fighter/tree/alpha-build), [beta](https://github.com/rhythm0708/sling-fighter/tree/beta-build), and a [full game loop](https://github.com/rhythm0708/sling-fighter/tree/full-game-loop) stage- all of which looks significantly different. Below I have compiled a document listing the name of each member and their individual contributions to the project. I hope that this is useful for grading, and for clarifying the work that has been done by each member. I truly believe that we all pulled our weight for this project and hopefully, this is reflected in our final game. Linked below.

[Vouch](https://docs.google.com/document/d/1aawnozYWJXqzhWCNuq6DW1AvTqfJCFlKV03uqT-946Q/edit?usp=sharing).

## User Interface and Input

**Describe your user interface and how it relates to gameplay. This can be done via the template.**
**Describe the default input configuration.**

**Add an entry for each platform or input style your project supports.**

## Movement/Physics

**Describe the basics of movement and physics in your game. Is it the standard physics model? What did you change or modify? Did you make your movement scripts that do not use the physics system?**

## Animation and Visuals

**List your assets, including their sources and licenses.**

**Describe how your work intersects with game feel, graphic design, and world-building. Include your visual style guide if one exists.**

## Wave System and Layout | Obstacle Behavior – Matvey Volkov

### Shift from Frontend Game Logic to Current Role
At the start of our project, our team decided to split up the role of Game Logic into two parts – frontend and backend. Having taken the former, I was initially tasked with integrating user-facing logic, such as player-enemy interaction and enemy behavior, as well as the wave system and layout. I was also in charge of handling scripts associated with the UI, particularly scene progression upon user interaction with the interface and advancement in game state. However, with time, certain responsibilities of my role have been taken on by other members of the group, such as UI-related scripting. Additionally, a decent portion of my code was rewritten in the two major refactors that were handled by Jethro (note that whenever I mention the said refactors, it is <b>*not*</b> my work). As a result, given that there was not much that I could modify in the new design patterns, I was left with the task of fulfilling other responsibilities of my role by building on top of the refactored code. In particular, my remaining responsibilities included designing the waves/levels and obstacle behavior. Through trial-and-error of my Frontend Game Logic implementation, I was able to experiment with different approaches to game rules and flow, providing my group members with testable product that could be discussed, evaluated, and revised. As a result, while a large portion of my code did not make it into the final build, it still allowed our team to rule out what worked and what did not, hence slowly paving the way to the current version of the game.

### Early Wave System

The [early wave system](https://github.com/rhythm0708/sling-fighter/blob/d1acf26aa281c16b9b2567b245cb813f86904e10/Assets/Scripts/GameManager.cs#L1) was decided to be an endless one and followed the [Factory Design Pattern](https://github.com/rhythm0708/sling-fighter/blob/d1acf26aa281c16b9b2567b245cb813f86904e10/Assets/Scripts/EnemyFactoryController.cs#L1), where enemies were instantiated in conformance to [specs](https://github.com/rhythm0708/sling-fighter/blob/d1acf26aa281c16b9b2567b245cb813f86904e10/Assets/Scripts/EnemySpec.cs#L1), just like in Exercise 4. Enemy spawn locations and specs were randomly generated for the sake of variety. Killing all the enemies generated at the start of the wave would advance one onto the next wave, with adjusted enemy count.

[Enemies](https://github.com/rhythm0708/sling-fighter/blob/d1acf26aa281c16b9b2567b245cb813f86904e10/Assets/Scripts/EnemyController.cs#L1) had a set amount of health, damage, and catch-up speed. They blindly gravitated towards the player, with the distinction between damaging the player and taking damage themselves being whether or not they collided with the player's fist. Enemies became dazed upon collision, and then continued their search. Our initial idea was to allow the player to enter slow-mo that would allow them to use a punching mechanic. Enemies destroyed themselves upon falling off the arena.

[Player](https://github.com/rhythm0708/sling-fighter/blob/d1acf26aa281c16b9b2567b245cb813f86904e10/Assets/Scripts/PlayerController.cs#L1) shared similar defining characteristics, and also had a damage cooldown in order to avoid continuous damage that would result in an instant game-end. Contact with enemies and bumpers (stationary objects that could be used to ricochet) would inflict damage.

In Jethro's first major refactor, the Factory Design Pattern would become scrapped and collider interactions would take on a more sophisticated form with the addition of hiboxes and hurtboxes; it also introduced a Publisher/Subscriber Design Pattern for OnHit and OnHurt actions. After some feedback during our meeting, I made it so that enemies would no longer spawn at random locations on the arena, but rather random enemies would spawn at randomly selected, fixed spawnpoints. GameObjects were be created for this purpose and assigned to a spawnpoint list in GameManager.
https://github.com/rhythm0708/sling-fighter/blob/6b845d5abff1e625a6f717621e22c0a32ec33593/Assets/Scripts/GameManager.cs#L1
With these changes came the addition of the transition to game over state (upon falling off the arena) and [retry functionality](https://github.com/rhythm0708/sling-fighter/blob/745bf32b6ab141cc4a49eae952df452e3c46f42c/Assets/UI%20Scripts/TryAgain.cs#L1).
Player now also [took damage](https://github.com/rhythm0708/sling-fighter/blob/745bf32b6ab141cc4a49eae952df452e3c46f42c/Assets/Scripts/PlayerController.cs#L1) and enemies had [different tracking speeds](https://github.com/rhythm0708/sling-fighter/blob/0fb211e7858f238e2916122adbdd7361969fb898/Assets/Scripts/Tracker.cs#L1) depending on their type.

### Enemy Rework and Wave System V.2

After trying out these changes, the team decided to go in a different direction. We decided to split the objects that the player interacts with into three categories: enemies, obstacles, and tools. Enemies would deal damage to the player upon interaction, and had very specific ways of being killed, one of which would be through the use of tools. Obstacles acted in a similar way, and interactions between these objects were handled by me using Jethro's hurtbox and hitbox system.

The [wave system](https://github.com/rhythm0708/sling-fighter/blob/6bbe89535e0ec2c1e1be12c7d0435e09bdd36a32/Assets/Scripts/GameManager.cs#L1) was essentially the same, save for the three different object types having different sets of spawn locations and variation in number spawned per wave (and how that number increased as the game progressed). Wave completion would be achieved upon killing all the enemies, which would then lead to a clean-up of all three object types, an update to the count of each, and generation in preparation for the next wave.

#### Enemies

The [Rusher](https://github.com/rhythm0708/sling-fighter/blob/6bbe89535e0ec2c1e1be12c7d0435e09bdd36a32/Assets/Scripts/Enemy%20Scripts/RusherScript.cs#L1) would "rush" the player, dealing damage upon contact. It could only be killed with the use of a tool.

The [Wizard](https://github.com/rhythm0708/sling-fighter/blob/6bbe89535e0ec2c1e1be12c7d0435e09bdd36a32/Assets/Scripts/Enemy%20Scripts/WizardScript.cs#L1) would shoot one homing [projectile](https://github.com/rhythm0708/sling-fighter/blob/6bbe89535e0ec2c1e1be12c7d0435e09bdd36a32/Assets/Scripts/Enemy%20Scripts/WizardProjectileScript.cs#L1) at the player at a time, and could only be destroyed upon collision with the player.

#### Obstacles

The [Orbiter](https://github.com/rhythm0708/sling-fighter/blob/6bbe89535e0ec2c1e1be12c7d0435e09bdd36a32/Assets/Scripts/OrbiterScript.cs#L1) was an object with a sphere hitbox rotating around it, with the hitbox being the main body that was at the center of the rotating system. There was a gap between the hitbox and hurtbox that the player could go through.

#### Tools

Tools would require activation in order to be used for the purpose of killing the Rusher.

The [Homing Missile](https://github.com/rhythm0708/sling-fighter/blob/2634f29ce29682b2dc4953a14ba652ee61dce3d7/Assets/Scripts/HomingMissileScript.cs#L1) would, upon collision with the player, guide itself to a random Rusher, and upon contact would destroy both itself and the latter.

The [Pillar](https://github.com/rhythm0708/sling-fighter/blob/6bbe89535e0ec2c1e1be12c7d0435e09bdd36a32/Assets/Scripts/PillarScript.cs#L1) would become activated upon collision with the player. The Rusher, guided into the Pillar, would be destroyed upon collision.

The Dummy had knockback physics that Jethro implemented, and could be knocked into the Rusher to destroy it. It is important to note, however, that the Dummy, just like all tools, had to be activated by contact with the player. That is to say, hitting a Dummy and not having it fall of the stage or collide with a Rusher would result in it essentially acting as a Pillar. The majority of the following code is not mine, but I implemented the OnHurt logic and initiation.
https://github.com/rhythm0708/sling-fighter/blob/2634f29ce29682b2dc4953a14ba652ee61dce3d7/Assets/Scripts/Knockback.cs#L48

### Final Wave System, Fixed Layouts and Obstacle Design

The final version of the game features a sequential progression through a series of predefined waves. The team decided to scrap the idea of using the enemy, obstacle, and tool triplet after the GDAC Playtester Event, and to stick to one damage-wise passive Dummy per wave, which was implemented by Jethro. I implemented the [basic version](https://github.com/rhythm0708/sling-fighter/blob/394b87bcd602498d89d451350c573c505403a7cb/Assets/Scripts/GameManager.cs#L1) of the wave system that was later built upon in the second major refactor. After that, I added in functionality for level selection in easter egg menu, and for retrying.

There is a total of fifteen waves in the current version of the game, and visual references to each can be found [here](https://drive.google.com/drive/folders/1OhQsjXMOsAeWCNaky4L6jDyxyEo_w0na?usp=sharing). One can also experience each of the levels by using the previously mentioned easter egg menu created by Chang.

#### Obstacles

Obstacles are indestructible and have fixed locations in each wave.

The Wizard was eventually renamed to officially be the Turret, but the internal name is still the same. It draws inspiration from _Quake_'s [Shalrath](https://quake.fandom.com/wiki/Vore), as it is mainly stationary and shoots homing [projectiles](https://github.com/rhythm0708/sling-fighter/blob/2c1a090b927d7f4093d06e9fc1941c3d623c440e/Assets/Scripts/Obstacles/WizardProjectileController.cs#L1) that deal damage to the player, but can be destroyed by either making them run into one-another, another obstacle (except for the Spikes), or the Dummy. Note that projectile code is a fusion of mine and Jethro's, as he refactored some of my tracking code, added in pulsation logic (sine-based), and modified my collision logic. It is not entirely my own. Jethro applied reflect logic to the obstacle, so contact causes the player to bounce off.

The [Oscillator](https://github.com/rhythm0708/sling-fighter/blob/2c1a090b927d7f4093d06e9fc1941c3d623c440e/Assets/Scripts/Obstacles/Oscillator.cs#L1) is a simple platform that moves side-to-side at the assigned speed. Jethro applied reflect logic to the obstacle, so contact causes the player to bounce off.

The now-obsolete [Smasher](https://github.com/rhythm0708/sling-fighter/blob/2c1a090b927d7f4093d06e9fc1941c3d623c440e/Assets/Scripts/Obstacles/Smasher.cs#L1) was inspired by [Thwomp](https://www.mariowiki.com/Thwomp). It essentially acted as a reversed version of the Spikes. The platform would fall after lingering for a set amount of time, and then linger at the ground before coming back up to its initial position. The process would repeat, and the platform was intended to deal damage to the player and potentially the Dummy. However, testing revealed visibility issues upon the platform coming down, so the obstacle served as a template for the Spikes.

#### Waves

Playtesting and feedback allowed me to revise the wave layouts in a way that appropriately challenges the player, while preserving the fun. There is a natural progression to the wave system in terms of difficulty, and each wave serves a purpose. The first two waves are meant to allow the player to familiarize themselves with the game's mechanics and movement system; wave one is a free-roam, and level two introduces the Pillars in such a way that the player can experiment with them without having to worry about any immediate danger. By placing an obstacle on the path from the starting point to the Dummy, Wave 3 encourages the player to explore the side-step mechanic and slinging to ropes on the sides. Waves 4 and 5 introduce new obstacles in a way that does feel overwhelming and allows the player to master dealing with them. Wave 6 marks the approximate half-way point in the game, and really tests the player's mastery of the core mechanics by introducing the Wizard, which requires dodging and savvy traversal of the environment in order to get rid of its projectiles. Wave 7 is less stressful in order to accomodate for the pressure put on the player in the previous wave, and difficulty ramps up with each successive wave. Wave 9 introduces the spikes, which are the main focus of waves 9-11. Waves 12-14 capture the culmination of all the different obstacles the player has encountered thus far, and tests them on their mastery of each. Wave 15, the final wave, introduces the Sweeper and is a challenging but less stressful wave than the previous three. It rewards the player for making it this far while introducing something new and interesting at the same time.

# Sub-Roles

## Audio

**List your assets, including their sources and licenses.**

**Describe the implementation of your audio system.**

**Document the sound style.** 

## Gameplay Testing – Matvey Volkov

All Observations and Playtester Comments forms can be found [here](https://drive.google.com/drive/folders/1lVFY1_KWUtM6W7QnpbIup4yGOXumRsbz?usp=sharing).

As a means of receiving objective feedback, I sought out a total of ten people that playtested our game during its various stages of development – the Alpha Build, the Beta Build, and the Final Build.

[Talk about findings]
[Talk about how we made adjustments to our game based on those findings]

## Game Logic (Backend) - Lucas Wang

For my sub-role, I worked on Game Logic (Backend) which encompasses data, randomization, scoring, and HUD logic. Admittedly the differences between frontend and backend game logic grew murky as the project progressed. Below are some of the main things that I worked on that fit broadly in the Game Logic (Backend) role:

### HUD Logic

I programmed and hooked up the HUD logic for our game. This includes:

- _Health Bar Controller_ - Inspired by Jet's prototype during GDAC, I developed `HealthBarController.cs` with a fader and no numerical values. The value of the health bar would immediately reduce, and the trail would follow after a calculated offset. I used `Mathf.Lerp` for this implementation. Jet later adapted it to work for all screen sizes. A link to the script can be found [here](https://github.com/rhythm0708/sling-fighter/blob/c7367e9394eafb7bd028e1af2005183167994dd8/Assets/Scripts/UI/HealthBarController.cs#L34).

- _Combo Controller_ - `ComboController.cs` originally had a pop-out animation, but I removed it last minute because it was too intrusive. The implementation in the final build simply increases in size and opacity at a positive nonzero combo value. There is a slight delay before the animation resets to 0. I also designed the system for tracking and incrementing the combo which can be found in `PlayerController.cs`. Put simply, it increments `OnHit()` and resets when the player attaches to a rope, unless the dummy has been knocked off the stage. This was done to make combo-ing easier and more achievable. [ComboController.cs](https://github.com/rhythm0708/sling-fighter/blob/c7367e9394eafb7bd028e1af2005183167994dd8/Assets/Scripts/UI/ComboController.cs#L1) and [PlayerController.cs](https://github.com/rhythm0708/sling-fighter/blob/9a2a64a4960f3e9a6c88b19462faca3fac26585c/Assets/Scripts/Player/PlayerController.cs#L54) are linked here.

- _Timer Controller_ - Players start each wave with 90s, and are tasked with beating it before time runs out. The animations for it was re-imagined during the refactor. My initial implementation was based on the idea that you could gain time by hitting enemies and was accompanied by a "PopOut" animation. Documentation of it can be found [here](https://github.com/rhythm0708/sling-fighter/blob/884d84db09b2bfef11d799d6551792e9db4ed0fc/Assets/Scripts/TimeManager.cs#L1) and [here](https://github.com/rhythm0708/sling-fighter/blob/884d84db09b2bfef11d799d6551792e9db4ed0fc/Assets/Scripts/UIManager.cs#L179), but it's pretty badly written and incomplete. The [new animation](https://github.com/rhythm0708/sling-fighter/blob/9a2a64a4960f3e9a6c88b19462faca3fac26585c/Assets/Scripts/UI/TimerController.cs#L1) made by Jet is well thought-out and aligned with our new game loop much better.

- _Results Screen_ - Between waves, players are privy to a snapshot of their `Wave [#] Score` and `Total Score`. I designed the transition between `waveCleared` and the results screen, which then naturally linked to the next wave. This involved work in [GameManager.cs](https://github.com/rhythm0708/sling-fighter/blob/9a2a64a4960f3e9a6c88b19462faca3fac26585c/Assets/Scripts/Game/GameManager.cs#L64) and [DisplayResults.cs](https://github.com/rhythm0708/sling-fighter/blob/9a2a64a4960f3e9a6c88b19462faca3fac26585c/Assets/Scripts/UI/DisplayResults.cs#L1). I designed the accompanying animation which causes the text to increase in opacity and shift slightly to the right. This was simple, but I thought it gave the screen a bit of life. I know that the score-displaying part isn't optimal, but unfortunately I never got around to changing it

### Systems

Some other systems that I programmed into the game:

- _Scoring System_ - I also designed the [scoring system](https://github.com/rhythm0708/sling-fighter/blob/9a2a64a4960f3e9a6c88b19462faca3fac26585c/Assets/Scripts/Game/GameManager.cs#L221) used throughout the game. The formula considers the time taken to beat the level, the highest combo, and the number of times that the player fell off the arena. It can go negative. The formula tended to produce somewhat 'random' values that still made sense according to how players played. I think that it incentivized 'good' gameplay whilst being weird enough so that players wouldn't dissect it.

- _Damage Engine_ - I programmed a simple damage engine that leaves room for mechanical expansion (in the form of power-ups, type effectiveness). For now, it calculates the damage based on `comboCount`. The scaling is 10-14-17- and so forth. It is a singleton. [`DamageEngine.cs`](https://github.com/rhythm0708/sling-fighter/blob/9a2a64a4960f3e9a6c88b19462faca3fac26585c/Assets/Scripts/Player/DamageEngine.cs#L1).

- (scrapped) _Score + Multiplier System, Lives System_ - For the alpha and beta builds, I made a live scoring system and a lives system to test out. The live scoring system would increment a 'held' score to the 'total' score when the multiplier ended. You would lose lives if the player traveled off the stage or got hit by an enemy. These systems worked in our previous vision of the game, but our new vision necessitated them getting scrapped. [ScoreManager.cs](https://github.com/rhythm0708/sling-fighter/blob/884d84db09b2bfef11d799d6551792e9db4ed0fc/Assets/Scripts/ScoreManager.cs#L1) and [LivesComponent.cs](https://github.com/rhythm0708/sling-fighter/blob/884d84db09b2bfef11d799d6551792e9db4ed0fc/Assets/Scripts/LivesComponent.cs#L1).

### Miscellaneous

There was some additional work I did that did not necessarily fit into my role. I attributed most of these instances to 'bug fixing' for my Producer role, but here are some honorable mentions that are at least somewhat relevant.

- (mostly scrapped) _SubscribeOnHit() / SubscribeOnHurt() / SubscribeOnBounds()_ - As we were working with entities that could both damage and be damaged by the player, our team found it useful to implement the Observer pattern in `Hitbox.cs` and `Hurtbox.cs` amongst other scripts. I either modified or helped to implement these systems (I don't remember) and adjusted the code to be able to communicate information back to listeners. Example [here](https://github.com/rhythm0708/sling-fighter/blob/884d84db09b2bfef11d799d6551792e9db4ed0fc/Assets/Scripts/Hurtbox.cs#L15).

- _ContextMenu for debugging_ - I wrote a `ContextMenu` which would instakill the dummy if you clicked a button in the editor. This was useful for me, at least, while playtesting and bug-fixing the project. Linked [here](https://github.com/rhythm0708/sling-fighter/blob/bbd0c03a3ef2d53075021c666f27e09d17c50e57/Assets/Scripts/Game/GameManager.cs#L231).

- _Fixed Pause and Easter Egg Menu_ - Chang created a pause system that does not stop time (like in Unrailed). I revamped the code to use public functions from `GameManager.cs` (to preserve encapsulation of variables) and disabled `PlayerController.cs` onPause(). I also increased font size of and spacing between buttons in the Easter Egg Scene. See [PauseMenu.cs](https://github.com/rhythm0708/sling-fighter/blob/275d14cf21beb816ac274ccea9e87cb58275cf19/Assets/Scripts/UI/PauseMenu.cs#L59) for an example of work.

As mentioned, the roles got pretty mixed up while developing this project. A lot of us did work on each other's scripts; a lot of wires were crossed. But these are the main things that I remember (and can find evidence of) doing for this project.

## Press Kit and Trailer

**Include links to your presskit materials and trailer.**

**Describe how you showcased your work. How did you choose what to show in the trailer? Why did you choose your screenshots?**

## Game Feel and Polish

**Document what you added to and how you tweaked your game to improve its game feel.**
