# bulls-and-cows.cs
An algorithm of the game:

### 1. A computer guesses an n-digit number consisting of non-repeating digits (no digit can be present in the number twice, the number cannot start from zero). To generate a random number you need to use the methods of the library class Random.
### 2. Then the user enters an n-digit number in order to guess the hidden number.
### 3. The computer displays a message about the quantity of numbers (cows) which are guessed, but not located in their places, and the quantity of numbers (bulls) which are guessed and stand in the appropriate places.
### 4. After that the user proceeds to item 2. The rounds continue until the user guesses the hidden number (i.e. get four “bulls").
