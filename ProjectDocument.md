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

Below is a template for you to highlight items of your work. These provide the evidence needed for your work to be evaluated. Try to have at least four such descriptions. They will be assessed on the quality of the underlying system and how they are linked to course content. 

*Short Description* - Long description of your work item that includes how it is relevant to topics discussed in class. [link to evidence in your repository](https://github.com/dr-jam/ECS189L/edit/project-description/ProjectDocumentTemplate.md)

Here is an example:  
*Procedural Terrain* - The game's background consists of procedurally generated terrain produced with Perlin noise. The game can modify this terrain at run-time via a call to its script methods. The intent is to allow the player to modify the terrain. This system is based on the component design pattern and the procedural content generation portions of the course. [The PCG terrain generation script](https://github.com/dr-jam/CameraControlExercise/blob/513b927e87fc686fe627bf7d4ff6ff841cf34e9f/Obscura/Assets/Scripts/TerrainGenerator.cs#L6).

You should replay any **bold text** with your relevant information. Liberally use the template when necessary and appropriate.

## Producer

As the producer, my primary responsibility was to manage the direction of our game and make sure that everyone understood their role. I did not want to take over the project and have the final say over every decision, 


One of my goals was for everyone to get the credit that they deserved for their work. I carefully looked over the [GameProject.md](https://github.com/dr-jam/GameplayProgramming/blob/master/GameProject.md) before assigning roles and tasks. For example, I ensured that our team received at least **10** pieces of feedback and they were shared to the rest of the team in a timely manner. I managed our [Google Drive](https://drive.google.com/drive/folders/1RleSwVFyKq-hQuMPw-QvtK3InfTamqMV?usp=sharing) as a central hub of communication and resources. 
I ensured that every person kept documentation for each of their roles, and that the Press Kit & Trailer was not left to the last minute. Another goal that I had was to Here are some more specific examples of work that I did:

- Meeting Notes - My meeting notes
- Rooms for Meetings - 
- Playtest Documents-
- Arranging Progress Reports Meetings -
- Merging Branches - 

**Describe the steps you took in your role as producer. Typical items include group scheduling mechanisms, links to meeting notes, descriptions of team logistics problems with their resolution, project organization tools (e.g., timelines, dependency/task tracking, Gantt charts, etc.), and repository management methodology.**

## User Interface and Input

**Describe your user interface and how it relates to gameplay. This can be done via the template.**
**Describe the default input configuration.**

**Add an entry for each platform or input style your project supports.**

## Movement/Physics/Refactor - Jethro Immanuel Peralta

**Describe the basics of movement and physics in your game. Is it the standard physics model? What did you change or modify? Did you make your movement scripts that do not use the physics system?**

### Framerate Independent Asymptotic Lerps
A common behavior we used throughout the project are asymptotic lerps. This is when we lerp a position every frame by some factor. For example, say we lerp a position half the distance every frame. This half distance will asymptotically approach zero each frame as the object approaches it's target position, resulting in a smooth and eased approach. A major issue with this however is framerate indepdendece: if we are lerping some distance every frame, we go faster or slower depending on the framerate. This was an issue I encountered when experimenting with a Lerp camera in Exercise 2, but my solution required hacky tricks with Rigidbody interpolation and FixedUpdate. To be able to do these lerps in a large project, I decided to read-up on solutions to this issue. 

I ended up finding [this resource by rorydiscool](https://www.rorydriscoll.com/2016/03/07/frame-rate-independent-damping-using-lerp/). He pointed out a common error where you multiply the lerp T value by `deltaTime`. While this scales according to framerate, it can cause values > 1 if `deltaTime` is high (low framerates). Additionally, it is not an accurate scaling for asmpytotic approaches. What he found instead, is that lerping in this way takes an exponential decay function. If we set this up as function that starts at time t and we want to find what the value is for t + n (n is deltaTime), we can solve to find the value the decay exponent must be. My math skills are fairly shaky to fully explain this, but we end up with a t = -ln(r). In Unity terms, we can achieve framerate independent asymptotic lerps by doing the following:
```
    value = Lerp(value, target, 1 - Mathf.Exp(-lambda * Time.deltaTime))
```
Lambda in this case, is the speed the value will approach its target. Note that this code is generic and the `Lerp` call and parameters change depending on what type of `value` is needed. I think one thing I could've done better however is provide a static helper function so we don't have to do the t calculation every time we want these lerps.

The main benefit of these asymptotic lerps in terms of feel is that we can have very quick and snappy actions by using a hight lambda value. However, the asymptotic nature of these lerps means there's a smooth easing for those snappy actions; they wouldn't disorient the player. It's also very robust, as we don't need to keep track of a timer or distance for these lerps to function.

### Slinging
The slinging physics system of the game does not use stock Unity physics, and instead uses a CharacterController to move the player. This allowed us to define the player's movement in a more precise fashion, which we needed for our unique gameplay mechanic. To make the movement when slinging feel good, I made sure the player retains velocity throughout the sling, giving them a high sense of speed and weight. 

Movement then, is handled in the PlayerMovement script. There are actually 3 states we consider for movement: waiting to sling (`WaitSling`), charging a sling (`ChargeSling`), and moving for a sling (`Move`). These states are enumerated and have an associated `Update` function to better separate the logic between states (still within the PlayerMovement script though). 

##### WaitSling
`WaitSling` is the most simple state: it simply idle's the player until they input Action (the A button on controller), which then transitions the movement to the `ChargeSling` state. There's some logic to recoil the player back to the center after slinging into a rope that also appears in `ChargeSling`, but this will be covered in the "Rope Physics" section. There is also a raycast down to keep the player on the ground.

##### ChargeSling
`ChargeSling` takes the player's stick/mouse input and pulls them back from the rope accordingly. To do this, we record the origin of the player relative the current rope when they enter it (no parenting is done). An offset is then added to this origin point based on the where the player is aiming with their controller.mouse. To get this vector however, we needed to do a little extra calculation in the GetAxisInput() function. This function returns a vector in where the stick is pointing in world space, **relative to the camera**. To do this, we get the forward and right vector's of the camera. Then, take the `Horizontal` and `Vertical` axis inputs and multiply them by their associated camera vectors (`Vertical` = forward, `Horizontal` = right).

Using the aim vector raw however, would appear jittery and unsmooth, as control stick/mouse movement tends to be very fast and imprecise. To fix this issue, we smooth out the axis input using asymptotically lerping another vector, called `smoothRopeAim` to the aim vector each frame. This results in a very smooth aiming experience that helps the game feel good to control. Below is a GIF of aiming without this smoothing:

![Unsmooth Aiming](./DocAssets/JitterAim.gif)

Once a player releases the `Action` button, the player is transitioned into moving sling if the direciton they want to go in is valid. This is determied if the dot product of the aim vector and the direction of the rope is greater than `0.25f`; we basically prevent anything less than perpindicular to the rope's direction from being valid. 

![Unsmooth Aiming](./DocAssets/SlingClamp.png)

However, a gamplay/feel issue arose as players tended to release the control stick slightly before the Action button. This means the aim vector would be zero and the sling would be invalid; player's would feel like the game dropped their sling input. To fix this, we store **20 of the most recent and valid aim vector's** in a list. If the player had a valid aim vector within **0.25 seconds** of releasing Action, we use the most recent and largest vector is the list. This way, players have a window where they can release their stick before releasing A and the sling would still go through.

##### Move
Finally the `Move` state move's the player based on where they aimed in ChargeSling. Most important, is that we record the vector the player aimed with from `ChargeSling` and consider it the forward vector for slinging until the player hits an obstacle. The player object then has a defined speed variable, and we move them by calling Move on the CharacterController with vector `forward * speed * Time.deltaTime`. Very simple, yet so effective!

The `Move` state also enables sidestepping. To do this, we have a single sidestep timer that becomes positive or negative depending on if the player inputted a left(-) or right(+) sidestep. This timer is then ticked down to zero by deltaTime. This same timer value then, is used to scale the speed of the sidestep (more recent input = larger timer = larger movement that frame). 

##### Bouncing Off Obstacles
A major feature of moving however is the fact that player's can bounce off obstacles. To accomplish this we detect for the CharacterController colliding with an object using `OnControllerColliderHit`. We then tag any object's that reflect the player with `Reflect`. Once a reflecting object is contacted, we flatten the contact normal to ensure reflections are on the XZ plane only. We then take the normalized of the player and calculate its reflection of the surface normal by calcuating the [vector of reflection](http://www.sunshine2k.de/articles/coding/vectorreflection/vectorreflection.html) which becomes the new forward vector.

#### The Dummy

##### Knockback
The dummy's physics are **very** similar to the player's movement physics. It uses a CharacterController to move around and travels in a straight line until it hits an obstacle. However, we needed the dummy to stop eventually so the player can hit it again, unlike the player who always moves. So, it has a single `knockbackVelocity` vector instead of a forward vector and speed. 

`knockbackVelocity` is set when it enters the player's hitbox trigger. This velocity is based on a defined speed scalar that sets how fast the dummy will initially move. For the direction of travel, it is a weighted combination of the vector from the player to the dummy, as well as the player's travel direction. Using the vector from the player to the dummy lets players get side hits, where the dummy goes sideways if the player hits it from its sides. However, it felt a little unintuitive at times, so we put slightly more weight on the player's travel direction. This is accomplished by lerping between the two vectors and having t be closer to the travel direction.

![Knockback System](./DocAssets/Knockback.png)

Unlike the player, the dummy must slow down and stop at some point so the player can hit it. To do this, an asymptotic lerp is used to bring the dummy's knockback velocity vector to zero. This is a simple way to simulate friction that feels realistic while also retaining some momentum. The dummy's velocity asymptotically aproaches 0 smoothly to give it a realistic feeling. I made it so we can define how fast approaches 0, to allow us to decide how "slippery" the dummy is.

The dummy also bounces off obstacles similar to the player. The code is nearly identical, the only difference is the dummy can also bounce of ropes.

##### Returning
Sometimes the dummy would end up in difficult spots after a sling and can even fall out of the arena. We wanted the dummy to return to the center to fix these problems and make the game a little easier, but having it teleport would be kinda off-putting. So, the dummy jumps to the center instead. 

Gravity is the component that makes jumpinig actually work. To do this, I created a GravityComponent that simulates gravity **only**. This simply tracks the gravity's scalar value and accelerates it based on an editor defined value. It also raycasts down to see if the object is on the ground, and sets the gravity to scalar to 0 accordingly.

The component also has a function to make it jump, where gravity is set to make it go up. However, the function actually returns how long it will take the object to reach the ground height after jumping. This allowed me to then calculate the exact speed the dummy had to be to reach the center for a given jump. So, returns call this jump function, then set the dummy's velocity to the proper value given the dummy's airtime

#### Rope Physics
The Rope physics/animation are the feature that I'm most proud of working on. These do a lot of work in giving the game the "slingy" feeling and make hitting the dummy into the rope's feel satisfying. There's a real sense of tactility and physicallity when using the ropes that makes them fun to just play around with.

##### The Shader
To begin with, the ropes have their own custom shader built using the URP shader graph. This is mainly to modify the vertices positions, as we want to be able to "pull back" on the rope mesh. Rope's are actually an insanely simple mesh: just a cube turned 45-degrees with 32 faces along the X-Axis. Note that the vertices have a range of -1 to 1 for their X-Axis positions. This is important for the shader to work. Below is an image of the rope's mesh

![Rope Mesh](./DocAssets/RopeMesh.png)

In the shader, we have a programmer-defined offset that acts as the apex point of the rope. We then apply this offset with varying strengths depending on how close the X-Position of a vertex is to zero. This is done by taking the absolute value of the X-Positions to obtain a value from 0 to 1 (since the X-Positions are -1 to 1). We then use this as the t-value to lerp the offset vector from zero-vector to its true value. This final lerped vector is added to the vertices original position to give the rope mesh its peak. Below is a the rope's vertex shader and a look into the programmer defined offset

![Rope Shader](./DocAssets/RopeShader.png)
![Defined Offset](./DocAssets/RopeOffset.gif)

##### The Physics Script
The `Rope` script then, defines the apex point for the shader to use. This apex point on the script also has its own velocity so it retains momentum. For the effect to work within the game however, the rope must always return to rest and be a straight line again. This is done by asymptotically lerping both the **position of the apex point** and its **velocity** to zero vectors. By lerping the position the rope eventually rests, but velocity still applies and moves it. By lerping the velocity then, the rope's apex point will retain momentum for a period of time till the velocity is zero. Once both the position and velocity are zero, the rope appears at rest. This system of velocity and position gives the ropes a sense of momentum, instead of feeling like a static object.

![Rope Shader](./DocAssets/RopePhysics.png)

Object's can attach to the Rope and become the apex point till detaching. Upon detaching, an object can also apply velocity to the rope, like when the player sligns off. Object's can also bounce the rope and give it a push at a position without attaching. This bounce applies velocity to simulate slamming into the ropes. By combining these behaviors with the rope returning to rest, we get a tactile feeling rope that is crucial to the feel of our game. 

Because the Rope does not position attached object's (the object's position the Rope's apex point). The player when attached to the rope has to position themselves to simulate recoil on the rope. This was a very simple solution: just have the player retain velocity upon entering the rope and record the point of entry when touching the rope. Then, the player's recoil velocity lerps to zero, and their position lerps to their point of entry, similar to how the rope's own apex point retains momentum.

#### A Major Refactor
##### Why We Needed It
About a week and a half before the Final Festival, we all came to the conclusion that we were confused about both the direction of the game and how the code is working. We did have an interesting set of mechanics and systems, but they were all tangled and incoherent in terms design and code implementation. Most challenging was that with each merge, the issue seemed to be getting worse. What we learned was, the confusion of the design and code were linked and we had to do something if we wanted to finish the game. After a productive meeting, we decided a refactor was necessary to clarify our design and code. To prevent issues with conflicting code, we decided one person should refactor to keep things consistent. I decided to take it on, as I had ample Unity experience and time.

##### The Principle: Design by Subtraction
In my approach to refactoring and thinking about the relationship between the design and code, I reminded myself of Fumito Ueda's approach to game design: [Design by Subtraction](https://www.youtube.com/watch?v=AmSBIyT0ih0). And if you look at the project's idea's from Alpha to the Final Build, you'll notice that there's actually more things removed/simplified than added (Matvey's section "Wave System and Layout | Obstacle Behavior" goes over most of these removals). This was important in clearing up our design confusion and was also applied in refactoring the code; we needed to remove code that was placed there "just in case we need it". 

SIDENOTE: One thing teamwise I was really appreciative of was how willing everyone was to remove things from the game they worked hard on. Matvey especially had lots of his code taken out. While there were concerns brought up, we found these decisions made the game better and also much easier to complete.

##### Consolidating The Game Rules: The GameManager
The major issue I noticed was we had many different scripts that all had something to do with the state of the "game rules" (ie. score, time, waves). I found that these were a pain point when merging and introducing new features, especially due to implicit behavior that requires looking at multiple files to understand. So, I decided to make them into one single GameManager script. Of course, this may introduce the issue of being a superclass, but the benefit of having the state of the "game rules" in one place where its explicit what's going on made this the right decision.

A major decision for the GameManager was to make it a Singleton. Singleton's tend to be a code-smell, but I found there were good reasons for our GameManager to be a Singleton. For one, the GameManager became a DontDestroyOnLoad object as we needed to keep track of game state between scenes, so it exists in every scene. Many objects in the game also needed to know the state of the game to operate (ie. timer for UI, wave number for results). There's also only **one** GameManager necessary to keep track of the game rules. Thus, a Singleton made sense for these reasons. Still, I made sure to protect it with encapsulation to ensure other objects can't break the state of the game.

Since our design change meant there's only the **player** fighting the **dummy** as the two major objects, these are readily accesible as get-only public properties. This made it easy for the other programmers to find get the two objects for their scripts. They could also access the wave number and timer as read-only, so defining behavior based on the game state was made much easier.

##### Hitboxes and Hurtboxes: Not Necessary
Early on, I introduced the concept of hurtboxes and hitboxes. This is mainly due to wanting robustness in how things contact one another and do damage(ie. weak spots, invincibility, etc). Seeing how games like Dark Souls and Street Fighter use the system, it at first seemed like the correct decision. However, I think it was probably the **most** confusing part of the code pre-refactor. It originally used a Pub-Sub pattern, where objects can subscribe to when a hitbox hits and when a hurtbox is hurt. However, this made us "crawl down the nest" where we had to untagle object hierarchies to determine what is contacting what. Even worse is that we didn't even benefit from the system; nothing more than "this object touched that one was needed". So, I took it out when refactoring.

Because we also simplified the "combat" of the game into just the player hitting the dummy (more in Matvey's section), we only needed to know if the dummy was hit by the player, and if the player hit the dummy. So, I still used the Pub-Sub pattern, and turned these into subscribable events. And because the dummy and player were exposed in the Singleton GameManager, we could simply access the GameManager instance to subscribe to these events. This overall made it much easier to program around the contact events of the game.

For any other collision events, the other programmers just had to have an OnTriggerEnter or OnCollisionEnter event depending if the object was one that physically pushed. Unity's systems for this were good enough and simple to program around.

**IN THE FUTURE: KEEP IT SIMPLE STUPID!**

##### Transition to New Code
To help the everyone else transition to the refactored code, I created a brief document that shows how to use the new code. It is available on [this Google Doc](https://docs.google.com/document/d/1Z_xVcOf1dmOaScEhO_Z6bmVBUq0mrpgzEkPmXDNcbGU/edit?usp=sharing).

FINAL NOTE: Lucas mentioned this earlier, but **everyone** in this project worked hard and was crucial to the team. Lots of stuff got removed in this refactor in terms of design, art, and code. Still, these initial works were crucial in helping us understand what our game is and how to make it better. Elements from these removed works are still present in the final verision of the game, and I want to make sure my group members get their due credit even with these removals.

## Animation and Visuals

**List your assets, including their sources and licenses.**

**Describe how your work intersects with game feel, graphic design, and world-building. Include your visual style guide if one exists.**

## Wave System and Layout | Obstacle Behavior – Matvey Volkov

### Shift from Frontend Game Logic to Current Role
At the start of our project, our team decided to split up the role of Game Logic into two parts – frontend and backend. Having taken the former, I was initially tasked with integrating user-facing logic, such as player-enemy interaction and enemy behavior, as well as the wave system and layout. I was also in charge of handling scripts associated with the UI, particularly scene progression upon user interaction with the interface and advancement in game state. However, with time, certain responsibilities of my role have been taken on by other members of the group, such as UI-related scripting for starting the game upon button press. Additionally, a decent portion of my code was rewritten in the two refactors that were handled by the Movement/Physics/Game Feel team member.

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

For my sub-role, I also worked on game logic (more towards the backend side).
**Document how the narrative is present in the game via assets, gameplay systems, and gameplay.** 

## Press Kit and Trailer

**Include links to your presskit materials and trailer.**

**Describe how you showcased your work. How did you choose what to show in the trailer? Why did you choose your screenshots?**

## Game Feel and Polish - Jethro Immanuel Peralta

**Document what you added to and how you tweaked your game to improve its game feel.**

Even though this is listed as sub-role, this to me felt like my main role, as I spent the most time getting the game to just intrisically feel good. 

### Movement and Physics Were Key
A core of the game feel was in the Physics/Movement: our mechanic of slinging needed to feel really good for the game to work. I've already went over most of the implmentation details in my main role section. In-terms of what makes this movement feel good, my main goal was to keep the player moving till they hit a rope. This give the game its snappy feeling as there's a "tension-and-release"; the player moves quickly and has to make quick sidesteps when slinging, then they have a pause to decide on their next sling when touching another rope. And again, the sense of tactility with the rope physics plays a big part in making the game feel good in a player's hands; there's a sense of physicality to the player hitting the ropes and slinging off them. 

As an overall for the Movement/Physics feel, nearly everything in the physics I programmed have a velocity value. I always try to keep this velocity retained in some way, rather than having it stop immediatly. This ensures eveything has a sense of momentum and weight.

A small detail for the Movement feel outside of its programming is the dust trail the player leaves behind while running. This gives a sense of traction to the player's run, and just gives an eye candy reward for simply slinging off the ropes. This is just a single ParticleEmitter that emits a translucent circle sprite. The emitter emits in world space so the particles stay in the location they spawned instead of following the player.

### The Camera is Snappy
Because the game is so fast, having the player manually control camera would've made the game too difficult. So, the camera is always facing the direction of the rope the player is on, or the direction the player travelling in a sling. I made sure the camera quickly gets to its target rotation without it being too disorienting. To do this, I again used an asymptotic lerp to get the camera to approach its target rotation with damping. This makes it get to its rotation quickly, while also having an ease-in so it's smooth and not disorienting.

### Make the Hits HIT
The second component of the game, hitting the dummy, also had to feel very good. Like the player, the dummy retains momentum so the player feels the power of the hits. The actual hits then have three components to make them feel **juicy**: hitstop/hitlag, screen shake, and the hit particles

For the hitstop/hitlag, I created a component called HitlagComponent. This script disables **all** MonoBehaviors on the GameObject and its children when hitlag starts. This basically freezes the GameObject momentarily till hitlag ends. Some scripts needed to be running even during hitlag, so I added the interface IIgnoreHitlag to keep certain scripts running evenn in hitlag. The effect in terms of game-feel, is that players can visually register a hit and feel its impact due to the freeze. Below is what the punches look like with no hitlag compared to with hitlag, the first feels noticably weaker:

![Hit Without Hitlag/Hitstop](./DocAssets/HitNoStun.gif)
![Hit With Hitlag/Hitstop](./DocAssets/HitStun.gif)

The screen shake then sells the power of a hit, as the player's strength is communicted in the camera. To really make things feels cohesive, this screen shake is actually directly linked to the hitlag. At the start of the hitlag, the camera shakes the most and it gets weaker and weaker till the hitlag is over. If these two were on separate timings, it would feel slightly off as it would be unclear what the "timing" of the hit is. The shake gives the hits the "crunch" they need to really feel strong (GIF might be too low FPS to notice, but this is basically just the final version of the hit in game):

![Hit Effect Sparks](./DocAssets/HitShake.gif)

Finally the hit particles give the player a visual flare for the hit that really make it pop. The visual effect is actually pretty simple, just two ParticleEmitters. The first emits its particles outwards using a trail particle. Trail particles basically stretch from its start position to its end position instead of being a single point in space. This creates the impact lines for the hit. These start off very large, the shrink to 0 to give the feeling of the hit "phasing-out".

![Hit Effect Lines](./DocAssets/HitSpikes.gif)

These impact lines also have a diamond shape at the tips. This makes it look like a "spark", as if the player is electrict when they are punching the dummy. These are also used in the second ParticleEmitter for the hit efefct

The second ParticleEmitter for the hit effect are the spark diamonds. These emit outwards as regular particle, but have a random rotation. Again, these diamonds give the "spark" sense to the hit effect, giving the hit impacts an eletricial strength to them.

![Hit Effect Sparks](./DocAssets/HitSparks.gif)

As an overall, the hit effects are kept very quick and snappy to match the feeling of the punches. As the final touch, I made sure the materials for the effects use additive blending. This makes them brighten the objects they are drawn over, so they feel like a flash of light in the scene. Below is a comparison between non-additive and additive blending for the hit effect:

![Hit Effect Without Additve Blending](./DocAssets/HitNonAdditive.gif)
![Hit Effect Final](./DocAssets/HitAll.gif)

### Savor the Final Blow
The final kill cam really makes players feel rewarded once they clear a wave. As corny as it can be, slow-mo does an amazing job of selling a moment and making someone really savor what's going on. The slow-mo on the kill cam dials up the hit effects from earlier as a player gets a longer look at the hitlag, shake, and hit effect. 

Furthermore, it switches to a different camera to capture more of the moment. This does two things to better show the final blow. It first zooms in with a low FOV so player's can see the hit. It also circles around the player to show the hit from more angles. These two help accomplish the same goal: let the player see the whole hit.

Chang, who did all our sounds, added the final icing to the kill cam with the echoey final blow sound. This just made it feel amazing to beat up that dummy.

### Animations and Shaders are the Icing
The animations were the final icing on the cake to really make the game feel "slingy". Notable is the animation style: the game uses frame-by-frame animation akin to the Spiderverse movies. This style allows the animations to focus more on posing than motion, since the frames hold longer for a player to see. Striking poses like punching then really register for a player. I also used **extreme** squash-and-stretch to really sell the movements, especially since the moments are so fast.

The punch animations specfically, are **very** extereme. For the punches to feel immediate and snappy, there is **no wind-up**. The punches simply start on the frame they hit. To compensate for the lack of wind-up then, extreme squash and stretch is used to really show the punch. 

![Punch Animation First Frame](./DocAssets/PunchFrame1.png)

A feature of the punch animations to make the combos feel good is that the player always alternates between left and right punches. If it were just the same punch animation repeating each new hit, it wouldn't really read as multiple different hits. To do this, the PlayerController has a PunchIndex that is either 0 or 1, and the Animator picks the punch animation accordingly.

![Punch Animator](./DocAssets/PunchStates.png)

To accompish the animation style at a technical level, it starts with using constant interpolation in Blender. This forces the keyframes to go one-by-one rather than smoothly interpolating between each keyframe. Unforunately, Unity automatically interpolates between keyframes upon importing an animation. However, you can disable this interpolation upon import by modifying the animation curve tangents to be 0. INSERT UNITY THREAD

Furthermore, the animator had to have no transition time's and just go to the next animation immediatly. This was much easier than the animation keyframes, as I just had to set a single value to 0.

Daniel, our modeler, also made models with **excellent** silhouttes and simple colors, making it easy for the character poses to be distrinct. I had to tweak the models so their topology deforms well for squash-and-stretch, but overall his models played a huge part in helping the animations register clearly. To help Daniel's models register more clearly and highlight their silhouttes, I created a cel shader that loosely follows a silhoutted style similar to the modern Donkey Kong Country games:

![Donkey Kong Country](./DocAssets/DonkeyKong.jpg)

However, these games are 2D perspective and a pure silhoutted style would not work in 3D. So, the shader still takes into account the lighting angle of the scene instead of being flat dark colors. I then use a two-tone cel shader to really focus on the light and shadow; this further highlights the silhouttes of Daniel's models. More unique to this cel shader though is it has a defined light color and dark color; most simply have a single albedo that gets darker in shadows. This allows us to have a single defined shadow color (#17526A) that really ups the contrast and cohesion of the scene, similar to the Donkey Country games. 

![Cel Shader](./DocAssets/CelShader.png)
![Defined Shadow Colors](./DocAssets/ShaderShadow.gif)

### Players Need to Know How to Play
One of the best things about our game is how simple its mechanics are, in fact a goal we had when coming with our initial plan is to make the game easy to pick up and play. However, players still needed to know how to control the game; it would feel bad to pick up the game with no explanation. I find that game tutorials work much better when the player is in the game and prompted with the tutorial text. This is why I put short in game tutorial prompts on the first wave.

The prompts are simply button sprite drawns above the player, with a TextMeshPro telling them what to do. This way, a player can quickly parse the information through symbols rather than words. Once the player actually performs the given action, the prompt disappears since they know what to do with the game.

One thing we noticed while watching others play is that many were ignoring the sidestep mechanic. This mechanic was **crucial** for players to get better at the game, but players would forget it exists unless they learn how good it is. This created a feedback loop that led to a lack of sidestepping. Some things I saw were:
- Players would not use the sidestep at all even when told it exists before playing
- Players would try to learn to use the sidestep, but do it on the ropes instead of when slinging
- Players would sidestep once and forget about it, not learning the uses of it in game

To fix this, I added a prompt for the sidestep. This prompt shows up **only when slinging** so players know that they can only do it in that state. Additionally, the sidestep prompt always appears if a player **slings 4 times in a row without sidestepping**. This acted as a reminder for players who didn't sidestep and encouraged them to do it often. For players who understood how good the sidestep is and use it constantly, the prompt simply doesn't appear so it doesn't annoy them.