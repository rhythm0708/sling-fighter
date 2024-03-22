# Sling! #

## Summary ##

_Sling Fighter!_ is a slingshot-style arcade game where you launch into a dummy at full force! Compete for the best score in this action-packed feel-good brawl. This game contains 15 waves and a local leaderboard system.

## Project Resources

[Itch page](https://slingfighter.itch.io/sling-fighter)
[Trailoir](https://youtube.com)  
[Press Kit](https://dopresskit.com/)  
[Proposal: make your own copy of the linked doc.](https://docs.google.com/document/d/1qwWCpMwKJGOLQ-rRJt8G8zisCa2XHFhv6zSWars0eWM/edit?usp=sharing)  

## Gameplay Explanation ##

This game was designed for controller, but it will also work for keyboard and mouse. Use 'A' and the 'left stick' to pull back on the ropes, and let go to fire onto enemies. 'LB' and 'RB' are your sidesteps.

**Add it here if you did work that should be factored into your grade but does not fit easily into the proscribed roles! Please include links to resources and descriptions of game-related material that does not fit into roles here.**

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

# One final note

As Producer, I believe that it is important for every member of the team to be fairly credited for the work that they did. Our game has gone through an [alpha stage](https://github.com/rhythm0708/sling-fighter/tree/alpha-build), a [beta stage](https://github.com/rhythm0708/sling-fighter/tree/beta-build), and a [full game loop](https://github.com/rhythm0708/sling-fighter/tree/full-game-loop) stage- all of which looks significantly different. Below I have compiled a document listing the name of each member and their individual contributions to the project. I hope that this is useful for grading, and for clarifying the work that has been done by each person. I truly believe that each member pulled their weight for this project and that this is reflected in our final product. Please 

Link to Vouch.


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
At the start of our project, our team decided to split up the role of Game Logic into two parts – frontend and backend. Having taken the former, I was initially tasked with integrating user-facing logic, such as player-enemy interaction and enemy behavior, as well as the wave system and layout. I was also in charge of handling However, with time, certain responsibilities of my role have been taken on by other members of my group, such as UI-related scripting (ex. start game upon button press in main menu), and a lot of my code was rewritten in the two refactors that were handled by the Movement/Physics/Game Feel team member.

The first of these introduced hitboxes and hurtboxes into the game, while my own code relied on 

[Talk about Alpha Build: factory design pattern, randomization of spawn locations]
[Talk about Beta Build: refactor, fixed spawn locations but randomization in terms of which enemies spawn there]
[Talk about Final Build: fixed wave layouts]

- From obstacles to obstacles, tools, and enemies, to obstacles and one main "enemy"
- Handling collisions before and after the refactor
- Handling obstacle behavior
- Setting up various wave systems and GameManager that starts out in an in-game state and transitions to end game

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

## Game Logic (Backend)

For my sub-role, I worked on backend game logic which encompassed data, randomization, scoring, and HUD logic. Admittedly the differences between frontend and backend game logic grew murky as the project progressed. Below are some of the main things that I worked on that fit broadly in the Game Logic (Backend) role:

# HUD Logic

I did a majority of the HUD UI for our game. This includes

- HUD LogicHealth Bar
- 

# Miscellaneous / Ambiguous

- Damage Engine
- Combos and Multipliers
- Alpha Build: (old) Score + Multiplier System, Lives System
- Beta Build: Timer Incrementation

## Press Kit and Trailer

**Include links to your presskit materials and trailer.**

**Describe how you showcased your work. How did you choose what to show in the trailer? Why did you choose your screenshots?**

## Game Feel and Polish

**Document what you added to and how you tweaked your game to improve its game feel.**
