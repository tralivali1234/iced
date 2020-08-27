/*
Copyright (C) 2018-2019 de4dot@gmail.com

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

#if FAST_FMT
using System.Collections.Generic;
using Iced.Intel;
using Iced.UnitTests.Intel.DecoderTests;
using Xunit;

namespace Iced.UnitTests.Intel.FormatterTests.Fast {
	public sealed class FormatterTest64 : FastFormatterTest {
		[Theory]
		[MemberData(nameof(Format_Data_Default))]
		void Format_Default(int index, InstructionInfo info, string formattedString) => FormatBase(index, info, formattedString, FormatterFactory.Create_Default());
		public static IEnumerable<object[]> Format_Data_Default => FormatterTestCases.GetFormatData(64, "Fast", "Default");

		[Theory]
		[MemberData(nameof(Format_Data_Inverted))]
		void Format_Inverted(int index, InstructionInfo info, string formattedString) => FormatBase(index, info, formattedString, FormatterFactory.Create_Inverted());
		public static IEnumerable<object[]> Format_Data_Inverted => FormatterTestCases.GetFormatData(64, "Fast", "Inverted");

#if ENCODER
		[Theory]
		[MemberData(nameof(Format_Data_NonDec_Default))]
		void Format_NonDec_Default(int index, Instruction info, string formattedString) => FormatBase(index, info, formattedString, FormatterFactory.Create_Default());
		public static IEnumerable<object[]> Format_Data_NonDec_Default => FormatterTestCases.GetFormatData(64, NonDecodedInstructions.Infos64, "Fast", "NonDec_Default");

		[Theory]
		[MemberData(nameof(Format_Data_NonDec_Inverted))]
		void Format_NonDec_Inverted(int index, Instruction info, string formattedString) => FormatBase(index, info, formattedString, FormatterFactory.Create_Inverted());
		public static IEnumerable<object[]> Format_Data_NonDec_Inverted => FormatterTestCases.GetFormatData(64, NonDecodedInstructions.Infos64, "Fast", "NonDec_Inverted");
#endif

		[Theory]
		[MemberData(nameof(Format_Data_Misc))]
		void Format_Misc(int index, InstructionInfo info, string formattedString) => FormatBase(index, info, formattedString, FormatterFactory.Create_Default());
		public static IEnumerable<object[]> Format_Data_Misc => FormatterTestCases.GetFormatData(64, "Fast", "Misc", isMisc: true);
	}
}
#endif
