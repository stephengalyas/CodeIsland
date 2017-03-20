/* Team Python          Last edit made by: Stephen Galyas           Created on: March 17, 2017         Last modified on: March 17, 2017            Published on: N/A
 * This is the ULTIMATE "god" script for the Main Character.
 * This class will contain all public variables that may be accessed throughout the game regardless of the level being played.
*/

using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// The ultimate god class for the player. This class is used throughout the game and is not level-dependent.
/// </summary>
public class PlayerGod
{
	/// <summary>
	/// A boolean that permits the player to move their character.
	/// </summary>
	public static bool canMove = true;
}

