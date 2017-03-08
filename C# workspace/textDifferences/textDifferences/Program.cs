using System;
using textDifferences;

namespace textDifferences
{
	class MainClass
	{
		public static void Main (string[] args)
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

	}
}
