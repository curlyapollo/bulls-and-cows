# bulls-and-cows.cs
The algorithm of the game:

1. The computer guesses an n-digit number consisting of non-repeating digits (no digit can be present in the number twice, the number cannot start from zero). To generate a random number, it is necessary to use the methods of the library class Random.
2. Then the user, trying to guess the hidden number, enters an n-digit number.
3. The computer displays a message about how many numbers (cows) are guessed, but not located in their places, and how many numbers (bulls) are guessed and are in their places.
4. After that, the user proceeds to item 2. The rounds continue until the
user guesses the hidden number (i.e. get
four “bulls").
