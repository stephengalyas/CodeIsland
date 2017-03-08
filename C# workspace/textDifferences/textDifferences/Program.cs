using System;
using textDifferences;

namespace textDifferences
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			StephenCode ();
		}

		private static int[] DiffCharCodes(string aText, bool ignoreCase) {
			int[] Codes;

			if (ignoreCase)
				aText = aText.ToUpperInvariant();

			Codes = new int[aText.Length];

			for (int n = 0; n < aText.Length; n++)
				Codes[n] = (int)aText[n];

			return (Codes);
		} // DiffCharCodes

		private static void DiffCode()
		{
			String original = "Hello there! I am Michael, coder and cool guy";
			String edited = "Hi there! I am michael, loser and terrible!";

			int[] originalCodes = DiffCharCodes (original, false);
			int [] editedCodes = DiffCharCodes (edited, false);

			Diff.Item[] diffs = Diff.DiffInt(originalCodes, editedCodes);

			int pos = 0;
			for (int n = 0; n < diffs.Length; n++) {
				Diff.Item it = diffs[n];

				// write unchanged chars
				while ((pos < it.StartB) && (pos < edited.Length)) {
					Console.Write(edited[pos]);
					pos++;
				} // while

				// write deleted chars
				if (it.deletedA > 0) {
					for (int m = 0; m < it.deletedA; m++) {
						Console.ForegroundColor = ConsoleColor.Red;
						Console.Write(original[it.StartA + m]);
					} // for

				}

				// write inserted chars
				if (pos < it.StartB + it.insertedB) {
					while (pos < it.StartB + it.insertedB) {
						Console.ForegroundColor = ConsoleColor.Green;
						Console.Write(edited[pos]);
						pos++;
					} // while
				} // if
			} // while

			// write rest of unchanged chars
			while (pos < edited.Length) {
				Console.Write(edited[pos]);
				pos++;
			} // while
		} // Close DiffCode().

		private static void StephenCode()
		{
			// Declare and instantiate the strings.
			string original = "Hello There Friend";
			string input = "Hello There Friend";
			string output = String.Empty;

			// Convert these strings to arrays.
			string[] origArray = original.Split(' ');
			string[] inpArray = input.Split(' ');

			// Check which string is shorter and store the appropriate length.
			//count = ((origArray.Length < inpArray.Length) ? origArray.Length : inpArray.Length);

			// If the input is less than the correct string, iterate through the original array until the index runs out of bounds in the input array.
			// Then, draw the correct number of underscores to indicate the missing characters.
			if (inpArray.Length < origArray.Length)
			{
				for (int i = 0; i < inpArray.Length; i++)
				{

					// Split the substrings into arrays of characters.
					char[] origChars = origArray[i].ToCharArray();
					char[] inpChars = inpArray[i].ToCharArray();

					// Check which substring at index 'i' is shorter. This will be the count for iterating through the substrings.
					if (origChars.Length < inpChars.Length)
					{ // User inputted too many characters.

						// Check each valid character. If they are not equal, print an underscore.
						for (int j = 0; j < origChars.Length; j++)
						{

							if (origChars[j] == inpChars[j])
							{

								// The characters match!
								output += inpChars[j];
							}
							else
							{

								// The characters do NOT match.
								output += '_';
							}
						}

						// Now, print a space.
						output += " ";

						// End of correct words. For the extra inputted values, print an astericks.
						for (int ex = 0; ex < (inpChars.Length - origChars.Length); ex++)
						{

							output += '*';
						}

					} // End condition (user entered too many values);

					else if (origChars.Length > inpChars.Length)
					{ // The user entered too few characters.

						// Check each valid character. If they are not equal, print an underscore.
						for (int j = 0; j < inpChars.Length; j++)
						{

							if (origChars[j] == inpChars[j])
							{

								// The characters match!
								output += inpChars[j];
							}
							else
							{

								// The characters do NOT match.
								output += '_';
							}
						}

					}  // Close condition (user entered too few values).

					else
					{ // The substrings are of equal length.

						// Check each valid character. If they are not equal, print an underscore.
						for (int j = 0; j < inpChars.Length; j++)
						{

							if (origChars[j] == inpChars[j])
							{

								// The characters match!
								output += inpChars[j];
							}
							else
							{

								// The characters do NOT match.
								output += '_';
							}
						}
					} // Close else condition (substrings are of equal length).

					// End of the substring. Print a space.
					output += " ";

				} // Close outer for loop.

				// Now, print underscores for the missing words.
				for (int i = 0; i < origArray.Length - inpArray.Length; i++)
				{

					// Process the missing words.
					for (int j = 0; j < origArray[i].Length; j++)
					{

						output += '_';
					}

					// End of substring. Print a space.
					output += " ";
				}

				// Write the output string to the console.
				Console.WriteLine(output);

			} // Close if user inputted fewer words than the correct string.

			else if (inpArray.Length > origArray.Length)
			{ // The user entered more words than required.

				for (int i = 0; i < origArray.Length; i++)
				{

					// Split the substrings into arrays of characters.
					char[] origChars = origArray[i].ToCharArray();
					char[] inpChars = inpArray[i].ToCharArray();

					// Check which substring at index 'i' is shorter. This will be the count for iterating through the substrings.
					if (origChars.Length < inpChars.Length)
					{ // User inputted too many characters.

						// Check each valid character. If they are not equal, print an underscore.
						for (int j = 0; j < origChars.Length; j++)
						{

							if (origChars[j] == inpChars[j])
							{

								// The characters match!
								output += inpChars[j];
							}
							else
							{

								// The characters do NOT match.
								output += '_';
							}
						}

						// End of correct word. For the extra inputted values, print an astericks.
						for (int ex = 0; ex < (inpChars.Length - origChars.Length); ex++)
						{

							output += '*';
						}

					} // End condition (user entered too many values);

					else if (origChars.Length > inpChars.Length)
					{ // The user entered too few characters.

						// Check each valid character. If they are not equal, print an underscore.
						for (int j = 0; j < inpChars.Length; j++)
						{

							if (origChars[j] == inpChars[j])
							{

								// The characters match!
								output += inpChars[j];
							}
							else
							{

								// The characters do NOT match.
								output += '_';
							}
						}



						// End of correct word. For the missing inputted values, print an underscore.
						for (int ex = 0; ex < (origChars.Length - inpChars.Length); ex++)
						{

							output += '_';
						}

					}  // Close condition (user entered too few values).

					else
					{ // The substrings are of equal length.

						// Check each valid character. If they are not equal, print an underscore.
						for (int j = 0; j < inpChars.Length; j++)
						{

							if (origChars[j] == inpChars[j])
							{

								// The characters match!
								output += inpChars[j];
							}
							else
							{

								// The characters do NOT match.
								output += '_';
							}
						}
					} // Close else condition (substrings are of equal length).

				} // Close outer for loop.

				// End of the substring. Print a space.
				output += " ";

				// Now, print astericks for the unnecessary words.
				for (int i = inpArray.Length - origArray.Length - 1; i < inpArray.Length; i++)
				{

					// Process the missing words.
					for (int j = 0; j < inpArray[i].Length; j++)
					{

						output += '*';
					}

					// End of substring. Print a space.
					output += " ";
				}

				// Write the output string to the console.
				Console.WriteLine(output);
			}

			else
			{ // The strings are of equal length.

				if (original == input) // The string are identical.
				{
					Console.WriteLine("The strings are identical!");
				}

				else
				{
					for (int i = 0; i < origArray.Length; i++)
					{

						// Split the substrings into arrays of characters.
						char[] origChars = origArray[i].ToCharArray();
						char[] inpChars = inpArray[i].ToCharArray();

						// Check which substring at index 'i' is shorter. This will be the count for iterating through the substrings.
						if (origChars.Length < inpChars.Length)
						{ // User inputted too many characters.

							// Check each valid character. If they are not equal, print an underscore.
							for (int j = 0; j < origChars.Length; j++)
							{

								if (origChars[j] == inpChars[j])
								{

									// The characters match!
									output += inpChars[j];
								}
								else
								{

									// The characters do NOT match.
									output += '_';
								}
							}

							// End of correct word. For the extra inputted values, print an astericks.
							for (int ex = 0; ex < (inpChars.Length - origChars.Length); ex++)
							{

								output += '*';
							}

						} // End condition (user entered too many values);

						else if (origChars.Length > inpChars.Length)
						{ // The user entered too few characters.

							// Check each valid character. If they are not equal, print an underscore.
							for (int j = 0; j < inpChars.Length; j++)
							{

								if (origChars[j] == inpChars[j])
								{

									// The characters match!
									output += inpChars[j];
								}
								else
								{

									// The characters do NOT match.
									output += '_';
								}
							}

							// End of correct word. For the missing inputted values, print an underscore.
							for (int ex = 0; ex < (origChars.Length - inpChars.Length); ex++)
							{

								output += '_';
							}

						}  // Close condition (user entered too few values).

						else
						{ // The substrings are of equal length.

							// Check each valid character. If they are not equal, print an underscore.
							for (int j = 0; j < inpChars.Length; j++)
							{

								if (origChars[j] == inpChars[j])
								{

									// The characters match!
									output += inpChars[j];
								}
								else
								{

									// The characters do NOT match.
									output += '_';
								}
							}
						} // Close else condition (substrings are of equal length).


						// End of the substring. Print a space.
						output += " ";

					} // Close outer for loop.


					// Write the output string to the console.
					Console.WriteLine(output);
				}

			}
		}
}
