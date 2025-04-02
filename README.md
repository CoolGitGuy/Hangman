Create a Hangman game in a C# console application.

Hangman Rules

1. Setup

1.1 The player has 5 lives
1.2 A random term is chosen
1.3 The letters of the term are represented by underscores (_) instead of actual letters, with spaces between words

2. Turn

2.1 The player selects a letter
2.2 The letter is inserted in place of the underscores that represent it in the term
2.3 If the letter is not in the term, the player loses 1 life
2.4 The player cannot repeat the same letter

3. End of the game

3.1 If the player loses all lives, they lose
3.2 If the player fills in all the letters of the term, they win
3.3 If the player guesses the entire term, they win

Task in the C# console application

From the user's perspective:
- When the player starts the application, the main menu is displayed
- They select a game type and start playing
- The player selects letters and guesses terms
- When they guess a term, they receive a message, and a new term is given
- The game continues until the player loses all lives
- The player is returned to the main menu

From the developer's perspective:
- Terms should be loaded from the database
- Records are stored and read from a single database

Scoring system:
+2 for a correct letter
+3 for each missing letter (without repetition) when the term is guessed
	  Example: Word Google -> G__gl_ -> +3 for o, +3 for e

Life system:
-1 for an incorrect term guess
-1 for selecting a letter that is not in the term
+1 for solving a term
+1 for every 50 points earned

The player starts with 5 lives. If the player makes a 5th mistake (without changing the number of lives), they lose.

Guessing during a round:
- If the player enters a single character, it counts as a letter guess; otherwise, it counts as a term guess
- After guessing a term, the player receives a message displaying the term, lives, round number, and score, then continues playing
- If no more terms are available, the player gets +10 points for each remaining life and receives a message about the game ending, and the game ends normally

Main menu and submenus:
- The player must enter the number of the option they wish to activate
- If the option does not exist or the input is not a number, the player receives an invalid input message
- After selecting a category, the game starts

```
1 New Game
	1 All terms
	2 Famous People
	3 Musical Instruments
	4 Animals
	5 Geography
	6 Brands
	0 Back
2 Records
	1 All terms
	2 Famous People
	3 Musical Instruments
	4 Animals
	5 Geography
	6 Brands
	0 Back
0 Log Out
```

Examples
Menu display:
```
 ________________________________________
|                                        |
|  Records                               |
|  1 All terms                           |
|  2 Famous People                       |
|  3 Musical Instruments                 |
|  4 Animals                             |
|  5 Geography                           |
|  6 Brands                              |
|                                        |
|  0 Back                                |
|________________________________________|
```

Displaying records:
```
 ________________________________________
|                                        |
| Records for category: Geography        |
| Points     Name                        |
| 15        Andrija                      |
| 12        Milica                       |
| 10        Nikola                       |
| 7         Bajaga                       |
| 2         xXx_Shadow_Wolf_xXx          |
|                                        |
| 1 Clear records                        |
| 0 Back                                 |
|________________________________________|
```

During a round with all fields filled:
```
 ________________________________________
|                                        |
|  Points: 12  Round: 3                  |
|                                        |
|   _____,    __A__ _A__B_ _C ___D_ _C_  |
|   | /  ;                               |
|   |/   O    GPIOYCVDBA                 |
|   |   /|\                              |
|   |   / \   A +2 points!               |
|   |                                    |
|  _|_        K                          |
|________________________________________|
```

Notes:
- This is the second time I am making this application, so It doesn't match the description. I wanted to make it harder for myself.
