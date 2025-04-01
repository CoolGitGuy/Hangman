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
	- When they guess a term, they receive a message and a new term is given
	- The game continues until the player loses all lives
	- Then, the top 5 records and the player's score are displayed
	- If the player makes it into the top 5 for that category, they can enter their name, and their record is saved
	- The player is returned to the main menu

From the developer's perspective:
	- Terms should be loaded from files
	- Each file represents a category of terms
	- Records are stored and read from a single file

Scoring system:
	+2 for a correct letter
	+3 for each missing letter (without repetition) when the term is guessed
	  Example: Word Google -> G__gl_ -> +3 for o, +3 for e

Life system:
	-1 for an incorrect term guess
	-1 for selecting a letter that is not in the term
	+1 for solving a term
	+1 for every 20 points earned

The player starts with 5 lives, although the drawing consists of 6 parts (head, body, 2 arms, 2 legs). If the player makes a 6th mistake (without changing the number of lives), they lose.

Guessing during a round:
	- If the player enters a single character, it counts as a letter guess; otherwise, it counts as a term guess
	- After guessing a term, the player receives a message displaying the term, lives, round number, and score, then continues playing
	- If no more terms are available, the player gets +10 points for each remaining life, receives a message about the game ending, and the game ends normally

Main menu and submenus:
	- The player must enter the number of the option they wish to activate
	- If the option does not exist or the input is not a number, the player receives an invalid input message
	- After selecting a category, the game starts

```
1 New Game
	1 All terms
	2 Select term category
		1 Famous People
		2 Musical Instruments
		3 Animals
		4 Geography
		5 Brands
		0 Back
	0 Back
2 Records
	1 All terms
	2 Famous People
	3 Musical Instruments
	4 Animals
	5 Geography
	6 Brands
	0 Back
0 Exit
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

Note for juniors:
	- The task scope is likely larger than expected
	- The goal is to develop a sense of workflow, project structure, and coding approach
	- If divided, this task could be split into 4 separate challenges, each sufficient for a top grade
	- Completion is not mandatory, but meeting at least 3 times for progress discussion is recommended
	- The focus is on code clarity and organization, not just correctness
	- In practice, debugging and brainstorming with colleagues is essential
	- The recommended approach is to collaborate with someone at a similar skill level
	- This is an opportunity to experience real-world programming teamwork
	- If you are not interested in the full task, try focusing on the part you find most engaging
	- Your progress depends on your effort and interest
	- If this task does not interest you, that's okay—it just means it's not the right challenge for you
	- Even if you complete only a small portion, you will have gained valuable experience
	- If you are not interested in the full task, try focusing on the part you find most engaging
	- Your progress depends on your effort and interest
	- If this task does not interest you, that's okay—it just means it's not the right challenge for you
	- Even if you complete only a small portion, you will have gained valuable experience
