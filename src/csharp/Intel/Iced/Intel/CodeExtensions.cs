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

namespace Iced.Intel {
	static class CodeExtensions {
		public static bool IgnoresSegment(this Code code) {
			switch (code) {
			// GENERATOR-BEGIN: IgnoresSegmentTable
			// ⚠️This was generated by GENERATOR!🦹‍♂️
			case Code.Lea_r16_m:
			case Code.Lea_r32_m:
			case Code.Lea_r64_m:
			case Code.Bndcl_bnd_rm32:
			case Code.Bndcl_bnd_rm64:
			case Code.Bndcu_bnd_rm32:
			case Code.Bndcu_bnd_rm64:
			case Code.Bndmk_bnd_m32:
			case Code.Bndmk_bnd_m64:
			case Code.Bndcn_bnd_rm32:
			case Code.Bndcn_bnd_rm64:
				return true;
			// GENERATOR-END: IgnoresSegmentTable
			default:
				return false;
			}
		}

		public static bool IgnoresIndex(this Code code) {
			switch (code) {
			// GENERATOR-BEGIN: IgnoresIndexTable
			// ⚠️This was generated by GENERATOR!🦹‍♂️
			case Code.Bndldx_bnd_mib:
			case Code.Bndstx_mib_bnd:
				return true;
			// GENERATOR-END: IgnoresIndexTable
			default:
				return false;
			}
		}

		public static bool IsTileStrideIndex(this Code code) {
			switch (code) {
			// GENERATOR-BEGIN: TileStrideIndexTable
			// ⚠️This was generated by GENERATOR!🦹‍♂️
#if !NO_VEX
			case Code.VEX_Tileloaddt1_tmm_sibmem:
			case Code.VEX_Tilestored_sibmem_tmm:
			case Code.VEX_Tileloadd_tmm_sibmem:
				return true;
#endif
			// GENERATOR-END: TileStrideIndexTable
			default:
				return false;
			}
		}
	}
}