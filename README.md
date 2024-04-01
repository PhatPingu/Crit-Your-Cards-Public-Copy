This is a slice of what this project looked like in January 2023. Since then, I have been working on it in my free time, updating it whenever possible.

As a small preview of what when behind the construction of this game, I'm gonna talk a little bit abotu some of the choices I made, when
planning and executing its code architecture.

Largely, the way that I've organized the project is as follows:
- There is a large focus on the use of Scriptable Objects.
- There are creatures, weapons (equipment), cards, and effects.
- The effects are nested within the cards.
- The cards are nested within the creatures.

The idea is to eliminate much of the design work since you can create a card by simply dropping in an effect that has already been previously made.
An example here would be:
  You make an effect that says       A: { Caster Attacks target }
  You make another that says         B: { Caster applies Poison to target }
  Then you make an effect that says  C: { Caster Heals 2HP }

By dropping A and B inside a Card Scriptable Object, you are essentially creating a card that does the following:
  { Attack target and apply Poison }
Similarly, if you were to drop A and C onto a Card Scriptable Object, you now create a card that reads:
  { Attack target. Caster Heals 2HP }

To further facilitate for the designers, each effect would have its own description that is automatically added to the card.
So let's look at our effects again; imagine the text shown is also the description of the card:
  A: { Caster Attacks target }
  B: { Caster applies Poison to target }
  C: { Caster Heals 2HP }

By simply adding A and C to a Card, the designer's card automatically inherits that text, allowing for quick prototyping and tests.
![image](https://github.com/PhatPingu/Crit-Your-Cards-Public-Copy/assets/85648352/62d060d2-1d53-4e0e-8760-596862a12712)

Of course, if the standard effect_tect does not fit with the designer's needs, they can always just overrite the text them selves
![image](https://github.com/PhatPingu/Crit-Your-Cards-Public-Copy/assets/85648352/0b40c9b8-d11e-4206-b4be-a71bfac70ed4)

A similar system exists for the tooltip, the VFX associated to that effect, the SFX, etc;

Ultimately as creatures, cards and effects change within the scene, no object is created or destroyed.
They are either hidden or revealed and as they data is modified as the Creature, Card and Effect Scriptable Objects are replaced


If you are Curious to know what it looks like today (April 2024), here is a screen of it in the Unity Editor
![image](https://github.com/PhatPingu/Crit-Your-Cards-Public-Copy/assets/85648352/dac81226-2594-4b52-98f8-b09ffa7f3e12)

And here is a picture of it when the game is running:
![image](https://github.com/PhatPingu/Crit-Your-Cards-Public-Copy/assets/85648352/3223cc03-a8d1-4ffc-af34-e895dce8f0f8)

