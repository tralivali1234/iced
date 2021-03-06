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

using System;
using System.Collections.Generic;
using System.Linq;
using Generator.IO;

namespace Generator.Tables.CSharp {
	[Generator(TargetLanguage.CSharp)]
	sealed class CSharpMemorySizeInfoTableGenerator {
		readonly IdentifierConverter idConverter;
		readonly GenTypes genTypes;

		public CSharpMemorySizeInfoTableGenerator(GeneratorContext generatorContext) {
			idConverter = CSharpIdentifierConverter.Create();
			genTypes = generatorContext.Types;
		}

		public void Generate() {
			var defs = genTypes.GetObject<MemorySizeDefs>(TypeIds.MemorySizeDefs).Defs;
			var filename = CSharpConstants.GetFilename(genTypes, CSharpConstants.IcedNamespace, "MemorySizeExtensions.cs");
			var sizeToIndex = new Dictionary<uint, uint>();
			uint index = 0;
			foreach (var size in defs.Select(a => a.Size).Distinct().OrderBy(a => a))
				sizeToIndex[size] = index++;
			const int SizeBits = 5;
			const ushort IsSigned = 0x8000;
			const uint SizeMask = (1U << SizeBits) - 1;
			const int SizeShift = 0;
			const int ElemSizeShift = SizeBits;
			if (sizeToIndex.Count > SizeMask)
				throw new InvalidOperationException();
			new FileUpdater(TargetLanguage.CSharp, "MemorySizeInfoTable", filename).Generate(writer => {
				var memSizeName = genTypes[TypeIds.MemorySize].Name(idConverter);
				foreach (var def in defs) {
					byte b0 = checked((byte)def.ElementType.Value);
					ushort value = checked((ushort)((sizeToIndex[def.Size] << SizeShift) | (sizeToIndex[def.ElementSize] << ElemSizeShift)));
					if ((value & IsSigned) != 0)
						throw new InvalidOperationException();
					if (def.IsSigned)
						value |= IsSigned;
					writer.WriteLine($"0x{b0:X2}, 0x{(byte)value:X2}, 0x{(byte)(value >> 8):X2},");
				}
			});
			new FileUpdater(TargetLanguage.CSharp, "ConstData", filename).Generate(writer => {
				writer.WriteLine($"const ushort {idConverter.Constant(nameof(IsSigned))} = {IsSigned};");
				writer.WriteLine($"const uint {idConverter.Constant(nameof(SizeMask))} = {SizeMask};");
				writer.WriteLine($"const int {idConverter.Constant(nameof(SizeShift))} = {SizeShift};");
				writer.WriteLine($"const int {idConverter.Constant(nameof(ElemSizeShift))} = {ElemSizeShift};");
				writer.WriteLine("var sizes = new ushort[] {");
				using (writer.Indent()) {
					foreach (var size in sizeToIndex.Select(a => a.Key).OrderBy(a => a))
						writer.WriteLine($"{size},");
				}
				writer.WriteLine("};");
			});
		}
	}
}
