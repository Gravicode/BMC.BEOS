//#define BINARY_SEARCH
//#define ASCII_SHORTCUT
#define DELAY_LOOKUP

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Diagnostics;
using System.Threading;
//using Microsoft.SPOT;

namespace System.Xml
{

    /// <internalonly/>
    // <devdoc>
    //  The XmlCharType class is used for quick character type recognition
    //  which is optimized for the first 127 ascii characters.
    // </devdoc>
    internal struct XmlCharType
    {
        // Whitespace chars -- Section 2.3 [3]
        // Letters -- Appendix B [84]
        // Starting NCName characters -- Section 2.3 [5] (Starting Name characters without ':')
        // NCName characters -- Section 2.3 [4]          (Name characters without ':')
        // Character data characters -- Section 2.2 [2]
        // PubidChar ::=  #x20 | #xD | #xA | [a-zA-Z0-9] | [-'()+,./:=?;!*#@$_%] Section 2.3 of spec
        internal const int fWhitespace = 1;
        internal const int fLetter = 2;
        internal const int fNCStartName = 4;
        internal const int fNCName = 8;
        internal const int fCharData = 16;
        internal const int fPublicId = 32;
        internal const int fText = 64;
        internal const int fAttrValue = 128;

        internal const char MaxAsciiChar = (char)255;

#if DELAY_LOOKUP
        private const uint CharPropertiesSize = 256;
#else
        private const uint CharPropertiesSize = (uint)char.MaxValue + 1;
#endif

        internal const string s_Whitespace =
            "\u0009\u000a\u000d\u000d\u0020\u0020";

        const string s_Letter =
            "\u0041\u005a\u0061\u007a\u00c0\u00d6\u00d8\u00f6" +
            "\u00f8\u0131\u0134\u013e\u0141\u0148\u014a\u017e" +
            "\u0180\u01c3\u01cd\u01f0\u01f4\u01f5\u01fa\u0217" +
            "\u0250\u02a8\u02bb\u02c1\u0386\u0386\u0388\u038a" +
            "\u038c\u038c\u038e\u03a1\u03a3\u03ce\u03d0\u03d6" +
            "\u03da\u03da\u03dc\u03dc\u03de\u03de\u03e0\u03e0" +
            "\u03e2\u03f3\u0401\u040c\u040e\u044f\u0451\u045c" +
            "\u045e\u0481\u0490\u04c4\u04c7\u04c8\u04cb\u04cc" +
            "\u04d0\u04eb\u04ee\u04f5\u04f8\u04f9\u0531\u0556" +
            "\u0559\u0559\u0561\u0586\u05d0\u05ea\u05f0\u05f2" +
            "\u0621\u063a\u0641\u064a\u0671\u06b7\u06ba\u06be" +
            "\u06c0\u06ce\u06d0\u06d3\u06d5\u06d5\u06e5\u06e6" +
            "\u0905\u0939\u093d\u093d\u0958\u0961\u0985\u098c" +
            "\u098f\u0990\u0993\u09a8\u09aa\u09b0\u09b2\u09b2" +
            "\u09b6\u09b9\u09dc\u09dd\u09df\u09e1\u09f0\u09f1" +
            "\u0a05\u0a0a\u0a0f\u0a10\u0a13\u0a28\u0a2a\u0a30" +
            "\u0a32\u0a33\u0a35\u0a36\u0a38\u0a39\u0a59\u0a5c" +
            "\u0a5e\u0a5e\u0a72\u0a74\u0a85\u0a8b\u0a8d\u0a8d" +
            "\u0a8f\u0a91\u0a93\u0aa8\u0aaa\u0ab0\u0ab2\u0ab3" +
            "\u0ab5\u0ab9\u0abd\u0abd\u0ae0\u0ae0\u0b05\u0b0c" +
            "\u0b0f\u0b10\u0b13\u0b28\u0b2a\u0b30\u0b32\u0b33" +
            "\u0b36\u0b39\u0b3d\u0b3d\u0b5c\u0b5d\u0b5f\u0b61" +
            "\u0b85\u0b8a\u0b8e\u0b90\u0b92\u0b95\u0b99\u0b9a" +
            "\u0b9c\u0b9c\u0b9e\u0b9f\u0ba3\u0ba4\u0ba8\u0baa" +
            "\u0bae\u0bb5\u0bb7\u0bb9\u0c05\u0c0c\u0c0e\u0c10" +
            "\u0c12\u0c28\u0c2a\u0c33\u0c35\u0c39\u0c60\u0c61" +
            "\u0c85\u0c8c\u0c8e\u0c90\u0c92\u0ca8\u0caa\u0cb3" +
            "\u0cb5\u0cb9\u0cde\u0cde\u0ce0\u0ce1\u0d05\u0d0c" +
            "\u0d0e\u0d10\u0d12\u0d28\u0d2a\u0d39\u0d60\u0d61" +
            "\u0e01\u0e2e\u0e30\u0e30\u0e32\u0e33\u0e40\u0e45" +
            "\u0e81\u0e82\u0e84\u0e84\u0e87\u0e88\u0e8a\u0e8a" +
            "\u0e8d\u0e8d\u0e94\u0e97\u0e99\u0e9f\u0ea1\u0ea3" +
            "\u0ea5\u0ea5\u0ea7\u0ea7\u0eaa\u0eab\u0ead\u0eae" +
            "\u0eb0\u0eb0\u0eb2\u0eb3\u0ebd\u0ebd\u0ec0\u0ec4" +
            "\u0f40\u0f47\u0f49\u0f69\u10a0\u10c5\u10d0\u10f6" +
            "\u1100\u1100\u1102\u1103\u1105\u1107\u1109\u1109" +
            "\u110b\u110c\u110e\u1112\u113c\u113c\u113e\u113e" +
            "\u1140\u1140\u114c\u114c\u114e\u114e\u1150\u1150" +
            "\u1154\u1155\u1159\u1159\u115f\u1161\u1163\u1163" +
            "\u1165\u1165\u1167\u1167\u1169\u1169\u116d\u116e" +
            "\u1172\u1173\u1175\u1175\u119e\u119e\u11a8\u11a8" +
            "\u11ab\u11ab\u11ae\u11af\u11b7\u11b8\u11ba\u11ba" +
            "\u11bc\u11c2\u11eb\u11eb\u11f0\u11f0\u11f9\u11f9" +
            "\u1e00\u1e9b\u1ea0\u1ef9\u1f00\u1f15\u1f18\u1f1d" +
            "\u1f20\u1f45\u1f48\u1f4d\u1f50\u1f57\u1f59\u1f59" +
            "\u1f5b\u1f5b\u1f5d\u1f5d\u1f5f\u1f7d\u1f80\u1fb4" +
            "\u1fb6\u1fbc\u1fbe\u1fbe\u1fc2\u1fc4\u1fc6\u1fcc" +
            "\u1fd0\u1fd3\u1fd6\u1fdb\u1fe0\u1fec\u1ff2\u1ff4" +
            "\u1ff6\u1ffc\u2126\u2126\u212a\u212b\u212e\u212e" +
            "\u2180\u2182\u3007\u3007\u3021\u3029\u3041\u3094" +
            "\u30a1\u30fa\u3105\u312c\u4e00\u9fa5\uac00\ud7a3";

        const string s_NCStartName =
            "\u003a\u0039\u0041\u005a\u005f\u005f\u0061\u007a" +
            "\u00c0\u00d6\u00d8\u00f6\u00f8\u0131\u0134\u013e" +
            "\u0141\u0148\u014a\u017e\u0180\u01c3\u01cd\u01f0" +
            "\u01f4\u01f5\u01fa\u0217\u0250\u02a8\u02bb\u02c1" +
            "\u0386\u0386\u0388\u038a\u038c\u038c\u038e\u03a1" +
            "\u03a3\u03ce\u03d0\u03d6\u03da\u03da\u03dc\u03dc" +
            "\u03de\u03de\u03e0\u03e0\u03e2\u03f3\u0401\u040c" +
            "\u040e\u044f\u0451\u045c\u045e\u0481\u0490\u04c4" +
            "\u04c7\u04c8\u04cb\u04cc\u04d0\u04eb\u04ee\u04f5" +
            "\u04f8\u04f9\u0531\u0556\u0559\u0559\u0561\u0586" +
            "\u05d0\u05ea\u05f0\u05f2\u0621\u063a\u0641\u064a" +
            "\u0671\u06b7\u06ba\u06be\u06c0\u06ce\u06d0\u06d3" +
            "\u06d5\u06d5\u06e5\u06e6\u0905\u0939\u093d\u093d" +
            "\u0958\u0961\u0985\u098c\u098f\u0990\u0993\u09a8" +
            "\u09aa\u09b0\u09b2\u09b2\u09b6\u09b9\u09dc\u09dd" +
            "\u09df\u09e1\u09f0\u09f1\u0a05\u0a0a\u0a0f\u0a10" +
            "\u0a13\u0a28\u0a2a\u0a30\u0a32\u0a33\u0a35\u0a36" +
            "\u0a38\u0a39\u0a59\u0a5c\u0a5e\u0a5e\u0a72\u0a74" +
            "\u0a85\u0a8b\u0a8d\u0a8d\u0a8f\u0a91\u0a93\u0aa8" +
            "\u0aaa\u0ab0\u0ab2\u0ab3\u0ab5\u0ab9\u0abd\u0abd" +
            "\u0ae0\u0ae0\u0b05\u0b0c\u0b0f\u0b10\u0b13\u0b28" +
            "\u0b2a\u0b30\u0b32\u0b33\u0b36\u0b39\u0b3d\u0b3d" +
            "\u0b5c\u0b5d\u0b5f\u0b61\u0b85\u0b8a\u0b8e\u0b90" +
            "\u0b92\u0b95\u0b99\u0b9a\u0b9c\u0b9c\u0b9e\u0b9f" +
            "\u0ba3\u0ba4\u0ba8\u0baa\u0bae\u0bb5\u0bb7\u0bb9" +
            "\u0c05\u0c0c\u0c0e\u0c10\u0c12\u0c28\u0c2a\u0c33" +
            "\u0c35\u0c39\u0c60\u0c61\u0c85\u0c8c\u0c8e\u0c90" +
            "\u0c92\u0ca8\u0caa\u0cb3\u0cb5\u0cb9\u0cde\u0cde" +
            "\u0ce0\u0ce1\u0d05\u0d0c\u0d0e\u0d10\u0d12\u0d28" +
            "\u0d2a\u0d39\u0d60\u0d61\u0e01\u0e2e\u0e30\u0e30" +
            "\u0e32\u0e33\u0e40\u0e45\u0e81\u0e82\u0e84\u0e84" +
            "\u0e87\u0e88\u0e8a\u0e8a\u0e8d\u0e8d\u0e94\u0e97" +
            "\u0e99\u0e9f\u0ea1\u0ea3\u0ea5\u0ea5\u0ea7\u0ea7" +
            "\u0eaa\u0eab\u0ead\u0eae\u0eb0\u0eb0\u0eb2\u0eb3" +
            "\u0ebd\u0ebd\u0ec0\u0ec4\u0f40\u0f47\u0f49\u0f69" +
            "\u10a0\u10c5\u10d0\u10f6\u1100\u1100\u1102\u1103" +
            "\u1105\u1107\u1109\u1109\u110b\u110c\u110e\u1112" +
            "\u113c\u113c\u113e\u113e\u1140\u1140\u114c\u114c" +
            "\u114e\u114e\u1150\u1150\u1154\u1155\u1159\u1159" +
            "\u115f\u1161\u1163\u1163\u1165\u1165\u1167\u1167" +
            "\u1169\u1169\u116d\u116e\u1172\u1173\u1175\u1175" +
            "\u119e\u119e\u11a8\u11a8\u11ab\u11ab\u11ae\u11af" +
            "\u11b7\u11b8\u11ba\u11ba\u11bc\u11c2\u11eb\u11eb" +
            "\u11f0\u11f0\u11f9\u11f9\u1e00\u1e9b\u1ea0\u1ef9" +
            "\u1f00\u1f15\u1f18\u1f1d\u1f20\u1f45\u1f48\u1f4d" +
            "\u1f50\u1f57\u1f59\u1f59\u1f5b\u1f5b\u1f5d\u1f5d" +
            "\u1f5f\u1f7d\u1f80\u1fb4\u1fb6\u1fbc\u1fbe\u1fbe" +
            "\u1fc2\u1fc4\u1fc6\u1fcc\u1fd0\u1fd3\u1fd6\u1fdb" +
            "\u1fe0\u1fec\u1ff2\u1ff4\u1ff6\u1ffc\u2126\u2126" +
            "\u212a\u212b\u212e\u212e\u2180\u2182\u3007\u3007" +
            "\u3021\u3029\u3041\u3094\u30a1\u30fa\u3105\u312c" +
            "\u4e00\u9fa5\uac00\ud7a3";

        const string s_NCName =
            "\u002d\u002e\u0030\u0039\u0041\u005a\u005f\u005f" +
            "\u0061\u007a\u00b7\u00b7\u00c0\u00d6\u00d8\u00f6" +
            "\u00f8\u0131\u0134\u013e\u0141\u0148\u014a\u017e" +
            "\u0180\u01c3\u01cd\u01f0\u01f4\u01f5\u01fa\u0217" +
            "\u0250\u02a8\u02bb\u02c1\u02d0\u02d1\u0300\u0345" +
            "\u0360\u0361\u0386\u038a\u038c\u038c\u038e\u03a1" +
            "\u03a3\u03ce\u03d0\u03d6\u03da\u03da\u03dc\u03dc" +
            "\u03de\u03de\u03e0\u03e0\u03e2\u03f3\u0401\u040c" +
            "\u040e\u044f\u0451\u045c\u045e\u0481\u0483\u0486" +
            "\u0490\u04c4\u04c7\u04c8\u04cb\u04cc\u04d0\u04eb" +
            "\u04ee\u04f5\u04f8\u04f9\u0531\u0556\u0559\u0559" +
            "\u0561\u0586\u0591\u05a1\u05a3\u05b9\u05bb\u05bd" +
            "\u05bf\u05bf\u05c1\u05c2\u05c4\u05c4\u05d0\u05ea" +
            "\u05f0\u05f2\u0621\u063a\u0640\u0652\u0660\u0669" +
            "\u0670\u06b7\u06ba\u06be\u06c0\u06ce\u06d0\u06d3" +
            "\u06d5\u06e8\u06ea\u06ed\u06f0\u06f9\u0901\u0903" +
            "\u0905\u0939\u093c\u094d\u0951\u0954\u0958\u0963" +
            "\u0966\u096f\u0981\u0983\u0985\u098c\u098f\u0990" +
            "\u0993\u09a8\u09aa\u09b0\u09b2\u09b2\u09b6\u09b9" +
            "\u09bc\u09bc\u09be\u09c4\u09c7\u09c8\u09cb\u09cd" +
            "\u09d7\u09d7\u09dc\u09dd\u09df\u09e3\u09e6\u09f1" +
            "\u0a02\u0a02\u0a05\u0a0a\u0a0f\u0a10\u0a13\u0a28" +
            "\u0a2a\u0a30\u0a32\u0a33\u0a35\u0a36\u0a38\u0a39" +
            "\u0a3c\u0a3c\u0a3e\u0a42\u0a47\u0a48\u0a4b\u0a4d" +
            "\u0a59\u0a5c\u0a5e\u0a5e\u0a66\u0a74\u0a81\u0a83" +
            "\u0a85\u0a8b\u0a8d\u0a8d\u0a8f\u0a91\u0a93\u0aa8" +
            "\u0aaa\u0ab0\u0ab2\u0ab3\u0ab5\u0ab9\u0abc\u0ac5" +
            "\u0ac7\u0ac9\u0acb\u0acd\u0ae0\u0ae0\u0ae6\u0aef" +
            "\u0b01\u0b03\u0b05\u0b0c\u0b0f\u0b10\u0b13\u0b28" +
            "\u0b2a\u0b30\u0b32\u0b33\u0b36\u0b39\u0b3c\u0b43" +
            "\u0b47\u0b48\u0b4b\u0b4d\u0b56\u0b57\u0b5c\u0b5d" +
            "\u0b5f\u0b61\u0b66\u0b6f\u0b82\u0b83\u0b85\u0b8a" +
            "\u0b8e\u0b90\u0b92\u0b95\u0b99\u0b9a\u0b9c\u0b9c" +
            "\u0b9e\u0b9f\u0ba3\u0ba4\u0ba8\u0baa\u0bae\u0bb5" +
            "\u0bb7\u0bb9\u0bbe\u0bc2\u0bc6\u0bc8\u0bca\u0bcd" +
            "\u0bd7\u0bd7\u0be7\u0bef\u0c01\u0c03\u0c05\u0c0c" +
            "\u0c0e\u0c10\u0c12\u0c28\u0c2a\u0c33\u0c35\u0c39" +
            "\u0c3e\u0c44\u0c46\u0c48\u0c4a\u0c4d\u0c55\u0c56" +
            "\u0c60\u0c61\u0c66\u0c6f\u0c82\u0c83\u0c85\u0c8c" +
            "\u0c8e\u0c90\u0c92\u0ca8\u0caa\u0cb3\u0cb5\u0cb9" +
            "\u0cbe\u0cc4\u0cc6\u0cc8\u0cca\u0ccd\u0cd5\u0cd6" +
            "\u0cde\u0cde\u0ce0\u0ce1\u0ce6\u0cef\u0d02\u0d03" +
            "\u0d05\u0d0c\u0d0e\u0d10\u0d12\u0d28\u0d2a\u0d39" +
            "\u0d3e\u0d43\u0d46\u0d48\u0d4a\u0d4d\u0d57\u0d57" +
            "\u0d60\u0d61\u0d66\u0d6f\u0e01\u0e2e\u0e30\u0e3a" +
            "\u0e40\u0e4e\u0e50\u0e59\u0e81\u0e82\u0e84\u0e84" +
            "\u0e87\u0e88\u0e8a\u0e8a\u0e8d\u0e8d\u0e94\u0e97" +
            "\u0e99\u0e9f\u0ea1\u0ea3\u0ea5\u0ea5\u0ea7\u0ea7" +
            "\u0eaa\u0eab\u0ead\u0eae\u0eb0\u0eb9\u0ebb\u0ebd" +
            "\u0ec0\u0ec4\u0ec6\u0ec6\u0ec8\u0ecd\u0ed0\u0ed9" +
            "\u0f18\u0f19\u0f20\u0f29\u0f35\u0f35\u0f37\u0f37" +
            "\u0f39\u0f39\u0f3e\u0f47\u0f49\u0f69\u0f71\u0f84" +
            "\u0f86\u0f8b\u0f90\u0f95\u0f97\u0f97\u0f99\u0fad" +
            "\u0fb1\u0fb7\u0fb9\u0fb9\u10a0\u10c5\u10d0\u10f6" +
            "\u1100\u1100\u1102\u1103\u1105\u1107\u1109\u1109" +
            "\u110b\u110c\u110e\u1112\u113c\u113c\u113e\u113e" +
            "\u1140\u1140\u114c\u114c\u114e\u114e\u1150\u1150" +
            "\u1154\u1155\u1159\u1159\u115f\u1161\u1163\u1163" +
            "\u1165\u1165\u1167\u1167\u1169\u1169\u116d\u116e" +
            "\u1172\u1173\u1175\u1175\u119e\u119e\u11a8\u11a8" +
            "\u11ab\u11ab\u11ae\u11af\u11b7\u11b8\u11ba\u11ba" +
            "\u11bc\u11c2\u11eb\u11eb\u11f0\u11f0\u11f9\u11f9" +
            "\u1e00\u1e9b\u1ea0\u1ef9\u1f00\u1f15\u1f18\u1f1d" +
            "\u1f20\u1f45\u1f48\u1f4d\u1f50\u1f57\u1f59\u1f59" +
            "\u1f5b\u1f5b\u1f5d\u1f5d\u1f5f\u1f7d\u1f80\u1fb4" +
            "\u1fb6\u1fbc\u1fbe\u1fbe\u1fc2\u1fc4\u1fc6\u1fcc" +
            "\u1fd0\u1fd3\u1fd6\u1fdb\u1fe0\u1fec\u1ff2\u1ff4" +
            "\u1ff6\u1ffc\u20d0\u20dc\u20e1\u20e1\u2126\u2126" +
            "\u212a\u212b\u212e\u212e\u2180\u2182\u3005\u3005" +
            "\u3007\u3007\u3021\u302f\u3031\u3035\u3041\u3094" +
            "\u3099\u309a\u309d\u309e\u30a1\u30fa\u30fc\u30fe" +
            "\u3105\u312c\u4e00\u9fa5\uac00\ud7a3";

        const string s_CharData =
            "\u0009\u000a\u000d\u000d\u0020\ud7ff\ue000\ufffd";

        const string s_PublicID =
            "\u000a\u000a\u000d\u000d\u0020\u0021\u0023\u0025" +
            "\u0027\u003b\u003d\u003d\u003f\u005a\u005f\u005f" +
            "\u0061\u007a";

        const string s_Text = // TextChar = CharData - { 0xA | 0xD | '<' | '&' | 0x9 | ']' | 0xDC00 - 0xDFFF }
            "\u0020\u0025\u0027\u003b\u003d\u005c\u005e\ud7ff\ue000\ufffd";

        const string s_AttrValue = // AttrValueChar = CharData - { 0xA | 0xD | 0x9 | '<' | '>' | '&' | '\'' | '"' | 0xDC00 - 0xDFFF }
            "\u0020\u0021\u0023\u0025\u0028\u003b\u003d\u003d\u003f\ud7ff\ue000\ufffd";

        private static byte[] s_CharProperties;
        internal byte[] charProperties;

        // ISSUE: rswaney : need to make InitInstance thread safe
        static void InitInstance()
        {

            byte[] chProps = new byte[CharPropertiesSize];
            s_CharProperties = chProps;

            SetProperties(s_Whitespace, fWhitespace);
            SetProperties(s_Letter, fLetter);
            SetProperties(s_NCStartName, fNCStartName);
            SetProperties(s_NCName, fNCName);
            SetProperties(s_CharData, fCharData);
            SetProperties(s_PublicID, fPublicId);
            SetProperties(s_Text, fText);
            SetProperties(s_AttrValue, fAttrValue);
        }

        private static void SetProperties(string ranges, byte value)
        {
            char[] rngs = ranges.ToCharArray();
            for (int p = 0; p < rngs.Length; p += 2)
            {
                for (int i = rngs[p], last = rngs[p + 1]; i <= last; i++)
                {
#if DELAY_LOOKUP
                    //only seed the table for values < 128
                    if (i >= CharPropertiesSize)
                        return;
#endif
                    s_CharProperties[i] |= value;
                }
            }
        }

        private XmlCharType(byte[] charProperties)
        {
            Debug.Assert(s_CharProperties != null);
            this.charProperties = charProperties;
        }

        internal static Object s_lock = new Object();

        public static XmlCharType Instance
        {
            get
            {
                if (s_CharProperties == null)
                {
                    lock (s_lock)
                    {
                        if (s_CharProperties == null)
                        {
                            InitInstance();
                        }
                    }
                }

                return new XmlCharType(s_CharProperties);
            }
        }

        // methods for efficiently querying the character type.
        public bool IsWhiteSpace(char ch)
        {
#if DELAY_LOOKUP
            bool ret;
            if (ch < CharPropertiesSize)
                return (charProperties[ch] & fWhitespace) != 0;
            ret = searchRangeString(ch, s_Whitespace);
#if DEBUG
            bool check = linearSearchRangeString(ch, s_Whitespace);
            Debug.Assert(ret == check, "no match for char " + ch);
#endif
            return ret;

#else
            return (charProperties[ch] & fWhitespace) != 0;
#endif
        }

        public bool IsLetter(char ch)
        {
#if DELAY_LOOKUP
            bool ret;
            if (ch < CharPropertiesSize)
                return (charProperties[ch] & fLetter) != 0;
            ret = searchRangeString(ch, s_Letter);
#if DEBUG
            bool check = linearSearchRangeString(ch, s_Letter);
            Debug.Assert(ret == check, "no match for char " + ch);
#endif
            return ret;
#else
            return (charProperties[ch] & fLetter) != 0;
#endif
        }

        public bool IsExtender(char ch)
        {
            return (ch == 0xb7);
        }

        public bool IsNCNameChar(char ch)
        {
#if DELAY_LOOKUP
            bool ret;
            if (ch < CharPropertiesSize)
                return (charProperties[ch] & fNCName) != 0;
            ret = searchRangeString(ch, s_NCName);
#if DEBUG
            bool check = linearSearchRangeString(ch, s_NCName);
            Debug.Assert(ret == check, "no match for char " + ch);
#endif
            return ret;
#else
            return (charProperties[ch] & fNCName) != 0;
#endif
        }

        public bool IsStartNCNameChar(char ch)
        {
#if DELAY_LOOKUP
            bool ret;
            if (ch < CharPropertiesSize)
                return (charProperties[ch] & fNCStartName) != 0;
            ret = searchRangeString(ch, s_NCStartName);
#if DEBUG
            bool check = linearSearchRangeString(ch, s_NCStartName);
            Debug.Assert(ret == check, "no match for char " + ch);
#endif
            return ret;
#else
            return (charProperties[ch] & fNCStartName) != 0;
#endif
        }

        public bool IsCharData(char ch)
        {
#if DELAY_LOOKUP
            bool ret;
            if (ch < CharPropertiesSize)
                return (charProperties[ch] & fCharData) != 0;
            ret = searchRangeString(ch, s_CharData);
#if DEBUG
            bool check = linearSearchRangeString(ch, s_CharData);
            Debug.Assert(ret == check, "no match for char " + ch);
#endif
            return ret;
#else
            return (charProperties[ch] & fCharData) != 0;
#endif
        }

        // [13] PubidChar ::=  #x20 | #xD | #xA | [a-zA-Z0-9] | [-'()+,./:=?;!*#@$_%] Section 2.3 of spec
        public bool IsPubidChar(char ch)
        {
#if DELAY_LOOKUP
            bool ret;
            if (ch < CharPropertiesSize)
                return (charProperties[ch] & fPublicId) != 0;
            ret = searchRangeString(ch, s_PublicID);
#if DEBUG
            bool check = linearSearchRangeString(ch, s_PublicID);
            Debug.Assert(ret == check, "no match for char " + ch);
#endif
            return ret;
#else
            return (charProperties[ch] & fPublicId) != 0;
#endif
        }

        // TextChar = CharData - { 0xA, 0xD, '<', '&', ']' }
        internal bool IsTextChar(char ch)
        {
#if DELAY_LOOKUP
            if (ch < CharPropertiesSize)
                return (charProperties[ch] & fText) != 0;
            return searchRangeString(ch, s_CharData);
#else
            return (charProperties[ch] & fText) != 0;
#endif
        }

        // AttrValueChar = CharData - { 0xA, 0xD, 0x9, '<', '>', '&', '\'', '"' }
        internal bool IsAttributeValueChar(char ch)
        {
#if DELAY_LOOKUP
            if (ch < CharPropertiesSize)
                return (charProperties[ch] & fAttrValue) != 0;
            return searchRangeString(ch, s_CharData);
#else
            return (charProperties[ch] & fAttrValue) != 0;
#endif
        }

        public bool IsNameChar(char ch)
        {
            return IsNCNameChar(ch) || ch == ':';
        }

        public bool IsStartNameChar(char ch)
        {
            return IsStartNCNameChar(ch) || ch == ':';
        }

        public bool IsDigit(char ch)
        {
            return (ch >= 0x30 && ch <= 0x39);
        }

        public bool IsHexDigit(char ch)
        {
            return (ch >= 0x30 && ch <= 0x39) || (ch >= 'a' && ch <= 'f') || (ch >= 'A' && ch <= 'F');
        }

        internal bool IsOnlyWhitespace(string str)
        {
            return IsOnlyWhitespaceWithPos(str) == -1;
        }

        internal int IsOnlyWhitespaceWithPos(string str)
        {
            if (str != null)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if ((charProperties[str[i]] & fWhitespace) == 0)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        internal bool IsName(string str)
        {
            if (str.Length == 0 || !IsStartNameChar(str[0]))
            {
                return false;
            }

            for (int i = 1; i < str.Length; i++)
            {
                if (!IsNameChar(str[i]))
                {
                    return false;
                }
            }

            return true;
        }

        internal bool IsNmToken(string str)
        {
            if (str.Length == 0)
            {
                return false;
            }

            for (int i = 0; i < str.Length; i++)
            {
                if ((charProperties[str[i]] & fNCName) == 0 && str[i] != ':')
                {
                    return false;
                }
            }

            return true;
        }

        internal int IsOnlyCharData(string str)
        {

            if (str != null)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if ((charProperties[str[i]] & fCharData) == 0)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        internal int IsPublicId(string str)
        {
            if (str != null)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if ((charProperties[str[i]] & fPublicId) == 0)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

#if !DESKTOP
#if DELAY_LOOKUP
        /*
          * This searches a string, which is treated as a range list.  If the
          * value is contained within the string, then true is returned.  False
          * is returned otherwise.
          */
        //I would kill for a parametric binary search.
        //Code is taken from Array::BinarySearch
        private static bool searchRangeString(char value, string str)
        {
            Debug.Assert(str != null, "string null");

            int lo = 0;
            int hi = str.Length - 1;

            //search for the exact value as a boundary in the range
            while (lo <= hi)
            {
                //the compiler can't do a simple bloody strength reduction.
                int i = (lo + hi) >> 1;
                //found it exactly
                if (str[i] == value)
                    return true;
                else if (str[i] < value)
                    lo = i + 1;
                else
                    hi = i - 1;
            }

            //value was too small.
            if (lo == 0)
            {
                return false;
            }

            //value was too big.
            Debug.Assert(lo <= str.Length, "lo is too big");
            if (lo == str.Length)
                return false;

            //we have a value nearest.  If lo is even, then its the low end of
            //a range.
            if ((lo % 2) == 0)
            {
                if (value < str[lo])
                    return false;
                else
                    return true;
            }
            else
            {
                if (value < str[lo])
                    return true;
                else
                    return false;
            }
        }

#if DEBUG
        /*
          * This function exists solely for debugging the above function.
          */
        private static bool linearSearchRangeString(char value, string str)
        {
            Debug.Assert((str.Length % 2) == 0, "length of string isn't even");
            for (int i = 0; i < str.Length; i += 2)
            {
                if ((str[i] <= value) && (str[i + 1] >= value))
                    return true;
            }

            return false;
        }

#endif // DEBUG
#endif // DELAY_LOOKUP
#endif // !DESKTOP
    }
}


