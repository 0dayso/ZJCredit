﻿// Decompiled with JetBrains decompiler
// Type: System.Text.ISCIIEncoding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
  [Serializable]
  internal class ISCIIEncoding : EncodingNLS, ISerializable
  {
    private static int[] UnicodeToIndicChar = new int[1135]{ 673, 674, 675, 0, 676, 677, 678, 679, 680, 681, 682, 4774, 686, 683, 684, 685, 690, 687, 688, 689, 691, 692, 693, 694, 695, 696, 697, 698, 699, 700, 701, 702, 703, 704, 705, 706, 707, 708, 709, 710, 711, 712, 713, 714, 715, 716, 717, 719, 720, 721, 722, 723, 724, 725, 726, 727, 728, 0, 0, 745, 4842, 730, 731, 732, 733, 734, 735, 4831, 739, 736, 737, 738, 743, 740, 741, 742, 744, 0, 0, 4769, 0, 8944, 0, 0, 0, 0, 0, 4787, 4788, 4789, 4794, 4799, 4800, 4809, 718, 4778, 4775, 4827, 4828, 746, 0, 753, 754, 755, 756, 757, 758, 759, 760, 761, 762, 13040, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 929, 930, 931, 0, 932, 933, 934, 935, 936, 937, 938, 5030, 0, 0, 939, 941, 0, 0, 943, 945, 947, 948, 949, 950, 951, 952, 953, 954, 955, 956, 957, 958, 959, 960, 961, 962, 963, 964, 965, 966, 0, 968, 969, 970, 971, 972, 973, 975, 0, 977, 0, 0, 0, 981, 982, 983, 984, 0, 0, 1001, 0, 986, 987, 988, 989, 990, 991, 5087, 0, 0, 992, 994, 0, 0, 996, 998, 1000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5055, 5056, 0, 974, 5034, 5031, 5083, 5084, 0, 0, 1009, 1010, 1011, 1012, 1013, 1014, 1015, 1016, 1017, 1018, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2978, 0, 0, 2980, 2981, 2982, 2983, 2984, 2985, 0, 0, 0, 0, 2987, 2989, 0, 0, 2992, 2993, 2995, 2996, 2997, 2998, 2999, 3000, 3001, 3002, 3003, 3004, 3005, 3006, 3007, 3008, 3009, 3010, 3011, 3012, 3013, 3014, 0, 3016, 3017, 3018, 3019, 3020, 3021, 3023, 0, 3025, 3026, 0, 3028, 3029, 0, 3031, 3032, 0, 0, 3049, 0, 3034, 3035, 3036, 3037, 3038, 0, 0, 0, 0, 3040, 3042, 0, 0, 3044, 3046, 3048, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7092, 7093, 7098, 7104, 0, 7113, 0, 0, 0, 0, 0, 0, 0, 3057, 3058, 3059, 3060, 3061, 3062, 3063, 3064, 3065, 3066, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2721, 2722, 2723, 0, 2724, 2725, 2726, 2727, 2728, 2729, 2730, 0, 2734, 0, 2731, 2733, 2738, 0, 2736, 2737, 2739, 2740, 2741, 2742, 2743, 2744, 2745, 2746, 2747, 2748, 2749, 2750, 2751, 2752, 2753, 2754, 2755, 2756, 2757, 2758, 0, 2760, 2761, 2762, 2763, 2764, 2765, 2767, 0, 2769, 2770, 0, 2772, 2773, 2774, 2775, 2776, 0, 0, 2793, 6890, 2778, 2779, 2780, 2781, 2782, 2783, 6879, 2787, 0, 2784, 2786, 2791, 0, 2788, 2790, 2792, 0, 0, 6817, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6826, 0, 0, 0, 0, 0, 2801, 2802, 2803, 2804, 2805, 2806, 2807, 2808, 2809, 2810, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1953, 1954, 1955, 0, 1956, 1957, 1958, 1959, 1960, 1961, 1962, 6054, 0, 0, 1963, 1965, 0, 0, 1968, 1969, 1971, 1972, 1973, 1974, 1975, 1976, 1977, 1978, 1979, 1980, 1981, 1982, 1983, 1984, 1985, 1986, 1987, 1988, 1989, 1990, 0, 1992, 1993, 1994, 1995, 1996, 1997, 1999, 0, 2001, 2002, 0, 0, 2005, 2006, 2007, 2008, 0, 0, 2025, 6122, 2010, 2011, 2012, 2013, 2014, 2015, 0, 0, 0, 2016, 2018, 0, 0, 2020, 2022, 2024, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6079, 6080, 0, 1998, 6058, 6055, 0, 0, 0, 0, 2033, 2034, 2035, 2036, 2037, 2038, 2039, 2040, 2041, 2042, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1186, 1187, 0, 1188, 1189, 1190, 1191, 1192, 1193, 0, 0, 0, 0, 1195, 1197, 0, 1199, 1200, 1201, 1203, 0, 0, 0, 1207, 1208, 0, 1210, 0, 1212, 1213, 0, 0, 0, 1217, 1218, 0, 0, 0, 1222, 1223, 1224, 0, 0, 0, 1228, 1229, 1231, 1232, 1233, 1234, 1235, 1236, 0, 1237, 1239, 1240, 0, 0, 0, 0, 1242, 1243, 1244, 1245, 1246, 0, 0, 0, 1248, 1249, 1250, 0, 1252, 1253, 1254, 1256, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1266, 1267, 1268, 1269, 1270, 1271, 1272, 1273, 1274, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1441, 1442, 1443, 0, 1444, 1445, 1446, 1447, 1448, 1449, 1450, 5542, 0, 1451, 1452, 1453, 0, 1455, 1456, 1457, 1459, 1460, 1461, 1462, 1463, 1464, 1465, 1466, 1467, 1468, 1469, 1470, 1471, 1472, 1473, 1474, 1475, 1476, 1477, 1478, 0, 1480, 1481, 1482, 1483, 1484, 1485, 1487, 1488, 1489, 1490, 0, 1492, 1493, 1494, 1495, 1496, 0, 0, 0, 0, 1498, 1499, 1500, 1501, 1502, 1503, 5599, 0, 1504, 1505, 1506, 0, 1508, 1509, 1510, 1512, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5546, 5543, 0, 0, 0, 0, 1521, 1522, 1523, 1524, 1525, 1526, 1527, 1528, 1529, 1530, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2210, 2211, 0, 2212, 2213, 2214, 2215, 2216, 2217, 2218, 6310, 0, 2219, 2220, 2221, 0, 2223, 2224, 2225, 2227, 2228, 2229, 2230, 2231, 2232, 2233, 2234, 2235, 2236, 2237, 2238, 2239, 2240, 2241, 2242, 2243, 2244, 2245, 2246, 0, 2248, 2249, 2250, 2251, 2252, 2253, 2255, 2256, 2257, 2258, 0, 2260, 2261, 2262, 2263, 2264, 0, 0, 0, 0, 2266, 2267, 2268, 2269, 2270, 2271, 6367, 0, 2272, 2273, 2274, 0, 2276, 2277, 2278, 2280, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6345, 0, 6314, 6311, 0, 0, 0, 0, 2289, 2290, 2291, 2292, 2293, 2294, 2295, 2296, 2297, 2298, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2466, 2467, 0, 2468, 2469, 2470, 2471, 2472, 2473, 2474, 6566, 0, 2475, 2476, 2477, 0, 2479, 2480, 2481, 2483, 2484, 2485, 2486, 2487, 2488, 2489, 2490, 2491, 2492, 2493, 2494, 2495, 2496, 2497, 2498, 2499, 2500, 2501, 2502, 0, 2504, 2505, 2506, 2507, 2508, 2509, 2511, 2512, 2513, 2514, 2515, 2516, 2517, 2518, 2519, 2520, 0, 0, 0, 0, 2522, 2523, 2524, 2525, 2526, 2527, 0, 0, 2528, 2529, 2530, 0, 2532, 2533, 2534, 2536, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6570, 6567, 0, 0, 0, 0, 2545, 2546, 2547, 2548, 2549, 2550, 2551, 2552, 2553, 2554 };
    private static byte[] SecondIndicByte = new byte[4]{ (byte) 0, (byte) 233, (byte) 184, (byte) 191 };
    private static int[] IndicMappingIndex = new int[12]{ -1, -1, 0, 1, 2, 3, 1, 4, 5, 6, 7, 8 };
    private static char[,,] IndicMapping = new char[9, 2, 96]{ { { char.MinValue, 'ँ', 'ं', 'ः', 'अ', 'आ', 'इ', 'ई', 'उ', 'ऊ', 'ऋ', 'ऎ', 'ए', 'ऐ', 'ऍ', 'ऒ', 'ओ', 'औ', 'ऑ', 'क', 'ख', 'ग', 'घ', 'ङ', 'च', 'छ', 'ज', 'झ', 'ञ', 'ट', 'ठ', 'ड', 'ढ', 'ण', 'त', 'थ', 'द', 'ध', 'न', 'ऩ', 'प', 'फ', 'ब', 'भ', 'म', 'य', 'य़', 'र', 'ऱ', 'ल', 'ळ', 'ऴ', 'व', 'श', 'ष', 'स', 'ह', char.MinValue, 'ा', 'ि', 'ी', 'ु', 'ू', 'ृ', 'ॆ', 'े', 'ै', 'ॅ', 'ॊ', 'ो', 'ौ', 'ॉ', '्', '़', '।', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, '०', '१', '२', '३', '४', '५', '६', '७', '८', '९', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue }, { char.MinValue, 'ॐ', char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'ऌ', 'ॡ', char.MinValue, char.MinValue, 'ॠ', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'क़', 'ख़', 'ग़', char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'ज़', char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'ड़', 'ढ़', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'फ़', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'ॢ', 'ॣ', char.MinValue, char.MinValue, 'ॄ', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, '\x200C', '\x200D', 'ऽ', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, '뢿', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue } }, { { char.MinValue, 'ঁ', 'ং', 'ঃ', 'অ', 'আ', 'ই', 'ঈ', 'উ', 'ঊ', 'ঋ', 'এ', 'এ', 'ঐ', 'ঐ', 'ও', 'ও', 'ঔ', 'ঔ', 'ক', 'খ', 'গ', 'ঘ', 'ঙ', 'চ', 'ছ', 'জ', 'ঝ', 'ঞ', 'ট', 'ঠ', 'ড', 'ঢ', 'ণ', 'ত', 'থ', 'দ', 'ধ', 'ন', 'ন', 'প', 'ফ', 'ব', 'ভ', 'ম', 'য', 'য়', 'র', 'র', 'ল', 'ল', 'ল', 'ব', 'শ', 'ষ', 'স', 'হ', char.MinValue, 'া', 'ি', 'ী', 'ু', 'ূ', 'ৃ', 'ে', 'ে', 'ৈ', 'ৈ', 'ো', 'ো', 'ৌ', 'ৌ', '্', '়', '.', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, '০', '১', '২', '৩', '৪', '৫', '৬', '৭', '৮', '৯', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue }, { char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'ঌ', 'ৡ', char.MinValue, char.MinValue, 'ৠ', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'ড়', 'ঢ়', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'ৢ', 'ৣ', char.MinValue, char.MinValue, 'ৄ', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, '\x200C', '\x200D', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue } }, { { char.MinValue, char.MinValue, 'ஂ', 'ஃ', 'அ', 'ஆ', 'இ', 'ஈ', 'உ', 'ஊ', char.MinValue, 'ஏ', 'ஏ', 'ஐ', 'ஐ', 'ஒ', 'ஓ', 'ஔ', 'ஔ', 'க', 'க', 'க', 'க', 'ங', 'ச', 'ச', 'ஜ', 'ஜ', 'ஞ', 'ட', 'ட', 'ட', 'ட', 'ண', 'த', 'த', 'த', 'த', 'ந', 'ன', 'ப', 'ப', 'ப', 'ப', 'ம', 'ய', 'ய', 'ர', 'ற', 'ல', 'ள', 'ழ', 'வ', 'ஷ', 'ஷ', 'ஸ', 'ஹ', char.MinValue, 'ா', 'ி', 'ீ', 'ு', 'ூ', char.MinValue, 'ெ', 'ே', 'ை', 'ை', 'ொ', 'ோ', 'ௌ', 'ௌ', '்', char.MinValue, '.', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, '0', '௧', '௨', '௩', '௪', '௫', '௬', '௭', '௮', '௯', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue }, { char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, '\x200C', '\x200D', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue } }, { { char.MinValue, 'ఁ', 'ం', 'ః', 'అ', 'ఆ', 'ఇ', 'ఈ', 'ఉ', 'ఊ', 'ఋ', 'ఎ', 'ఏ', 'ఐ', 'ఐ', 'ఒ', 'ఓ', 'ఔ', 'ఔ', 'క', 'ఖ', 'గ', 'ఘ', 'ఙ', 'చ', 'ఛ', 'జ', 'ఝ', 'ఞ', 'ట', 'ఠ', 'డ', 'ఢ', 'ణ', 'త', 'థ', 'ద', 'ధ', 'న', 'న', 'ప', 'ఫ', 'బ', 'భ', 'మ', 'య', 'య', 'ర', 'ఱ', 'ల', 'ళ', 'ళ', 'వ', 'శ', 'ష', 'స', 'హ', char.MinValue, 'ా', 'ి', 'ీ', 'ు', 'ూ', 'ృ', 'ె', 'ే', 'ై', 'ై', 'ొ', 'ో', 'ౌ', 'ౌ', '్', char.MinValue, '.', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, '౦', '౧', '౨', '౩', '౪', '౫', '౬', '౭', '౮', '౯', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue }, { char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'ఌ', 'ౡ', char.MinValue, char.MinValue, 'ౠ', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'ౄ', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, '\x200C', '\x200D', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue } }, { { char.MinValue, 'ଁ', 'ଂ', 'ଃ', 'ଅ', 'ଆ', 'ଇ', 'ଈ', 'ଉ', 'ଊ', 'ଋ', 'ଏ', 'ଏ', 'ଐ', 'ଐ', 'ଐ', 'ଓ', 'ଔ', 'ଔ', 'କ', 'ଖ', 'ଗ', 'ଘ', 'ଙ', 'ଚ', 'ଛ', 'ଜ', 'ଝ', 'ଞ', 'ଟ', 'ଠ', 'ଡ', 'ଢ', 'ଣ', 'ତ', 'ଥ', 'ଦ', 'ଧ', 'ନ', 'ନ', 'ପ', 'ଫ', 'ବ', 'ଭ', 'ମ', 'ଯ', 'ୟ', 'ର', 'ର', 'ଲ', 'ଳ', 'ଳ', 'ବ', 'ଶ', 'ଷ', 'ସ', 'ହ', char.MinValue, 'ା', 'ି', 'ୀ', 'ୁ', 'ୂ', 'ୃ', 'େ', 'େ', 'ୈ', 'ୈ', 'ୋ', 'ୋ', 'ୌ', 'ୌ', '୍', '଼', '.', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, '୦', '୧', '୨', '୩', '୪', '୫', '୬', '୭', '୮', '୯', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue }, { char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'ఌ', 'ౡ', char.MinValue, char.MinValue, 'ౠ', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'ଡ଼', 'ଢ଼', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'ౄ', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, '\x200C', '\x200D', 'ଽ', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue } }, { { char.MinValue, char.MinValue, 'ಂ', 'ಃ', 'ಅ', 'ಆ', 'ಇ', 'ಈ', 'ಉ', 'ಊ', 'ಋ', 'ಎ', 'ಏ', 'ಐ', 'ಐ', 'ಒ', 'ಓ', 'ಔ', 'ಔ', 'ಕ', 'ಖ', 'ಗ', 'ಘ', 'ಙ', 'ಚ', 'ಛ', 'ಜ', 'ಝ', 'ಞ', 'ಟ', 'ಠ', 'ಡ', 'ಢ', 'ಣ', 'ತ', 'ಥ', 'ದ', 'ಧ', 'ನ', 'ನ', 'ಪ', 'ಫ', 'ಬ', 'ಭ', 'ಮ', 'ಯ', 'ಯ', 'ರ', 'ಱ', 'ಲ', 'ಳ', 'ಳ', 'ವ', 'ಶ', 'ಷ', 'ಸ', 'ಹ', char.MinValue, 'ಾ', 'ಿ', 'ೀ', 'ು', 'ೂ', 'ೃ', 'ೆ', 'ೇ', 'ೈ', 'ೈ', 'ೊ', 'ೋ', 'ೌ', 'ೌ', '್', char.MinValue, '.', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, '೦', '೧', '೨', '೩', '೪', '೫', '೬', '೭', '೮', '೯', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue }, { char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'ಌ', 'ೡ', char.MinValue, char.MinValue, 'ೠ', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'ೞ', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'ೄ', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, '\x200C', '\x200D', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue } }, { { char.MinValue, char.MinValue, 'ം', 'ഃ', 'അ', 'ആ', 'ഇ', 'ഈ', 'ഉ', 'ഊ', 'ഋ', 'എ', 'ഏ', 'ഐ', 'ഐ', 'ഒ', 'ഓ', 'ഔ', 'ഔ', 'ക', 'ഖ', 'ഗ', 'ഘ', 'ങ', 'ച', 'ഛ', 'ജ', 'ഝ', 'ഞ', 'ട', 'ഠ', 'ഡ', 'ഢ', 'ണ', 'ത', 'ഥ', 'ദ', 'ധ', 'ന', 'ന', 'പ', 'ഫ', 'ബ', 'ഭ', 'മ', 'യ', 'യ', 'ര', 'റ', 'ല', 'ള', 'ഴ', 'വ', 'ശ', 'ഷ', 'സ', 'ഹ', char.MinValue, 'ാ', 'ി', 'ീ', 'ു', 'ൂ', 'ൃ', 'െ', 'േ', 'ൈ', 'ൈ', 'ൊ', 'ോ', 'ൌ', 'ൌ', '്', char.MinValue, '.', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, '൦', '൧', '൨', '൩', '൪', '൫', '൬', '൭', '൮', '൯', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue }, { char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'ഌ', 'ൡ', char.MinValue, char.MinValue, 'ൠ', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, '\x200C', '\x200D', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue } }, { { char.MinValue, 'ઁ', 'ં', 'ઃ', 'અ', 'આ', 'ઇ', 'ઈ', 'ઉ', 'ઊ', 'ઋ', 'એ', 'એ', 'ઐ', 'ઍ', 'ઍ', 'ઓ', 'ઔ', 'ઑ', 'ક', 'ખ', 'ગ', 'ઘ', 'ઙ', 'ચ', 'છ', 'જ', 'ઝ', 'ઞ', 'ટ', 'ઠ', 'ડ', 'ઢ', 'ણ', 'ત', 'થ', 'દ', 'ધ', 'ન', 'ન', 'પ', 'ફ', 'બ', 'ભ', 'મ', 'ય', 'ય', 'ર', 'ર', 'લ', 'ળ', 'ળ', 'વ', 'શ', 'ષ', 'સ', 'હ', char.MinValue, 'ા', 'િ', 'ી', 'ુ', 'ૂ', 'ૃ', 'ે', 'ે', 'ૈ', 'ૅ', 'ો', 'ો', 'ૌ', 'ૉ', '્', '઼', '.', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, '૦', '૧', '૨', '૩', '૪', '૫', '૬', '૭', '૮', '૯', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue }, { char.MinValue, 'ૐ', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'ૠ', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'ૄ', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, '\x200C', '\x200D', 'ઽ', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue } }, { { char.MinValue, char.MinValue, 'ਂ', char.MinValue, 'ਅ', 'ਆ', 'ਇ', 'ਈ', 'ਉ', 'ਊ', char.MinValue, 'ਏ', 'ਏ', 'ਐ', 'ਐ', 'ਐ', 'ਓ', 'ਔ', 'ਔ', 'ਕ', 'ਖ', 'ਗ', 'ਘ', 'ਙ', 'ਚ', 'ਛ', 'ਜ', 'ਝ', 'ਞ', 'ਟ', 'ਠ', 'ਡ', 'ਢ', 'ਣ', 'ਤ', 'ਥ', 'ਦ', 'ਧ', 'ਨ', 'ਨ', 'ਪ', 'ਫ', 'ਬ', 'ਭ', 'ਮ', 'ਯ', 'ਯ', 'ਰ', 'ਰ', 'ਲ', 'ਲ਼', 'ਲ਼', 'ਵ', 'ਸ਼', 'ਸ਼', 'ਸ', 'ਹ', char.MinValue, 'ਾ', 'ਿ', 'ੀ', 'ੁ', 'ੂ', char.MinValue, 'ੇ', 'ੇ', 'ੈ', 'ੈ', 'ੋ', 'ੋ', 'ੌ', 'ੌ', '੍', '਼', '.', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, '੦', '੧', '੨', '੩', '੪', '੫', '੬', '੭', '੮', '੯', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue }, { char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'ਖ਼', 'ਗ਼', char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'ਜ਼', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'ੜ', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, 'ਫ਼', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, '\x200C', '\x200D', char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue } } };
    private const int CodeDefault = 0;
    private const int CodeRoman = 1;
    private const int CodeDevanagari = 2;
    private const int CodeBengali = 3;
    private const int CodeTamil = 4;
    private const int CodeTelugu = 5;
    private const int CodeAssamese = 6;
    private const int CodeOriya = 7;
    private const int CodeKannada = 8;
    private const int CodeMalayalam = 9;
    private const int CodeGujarati = 10;
    private const int CodePunjabi = 11;
    private const int MultiByteBegin = 160;
    private const int IndicBegin = 2305;
    private const int IndicEnd = 3439;
    private const byte ControlATR = 239;
    private const byte ControlCodePageStart = 64;
    private const byte Virama = 232;
    private const byte Nukta = 233;
    private const byte DevenagariExt = 240;
    private const char ZWNJ = '\x200C';
    private const char ZWJ = '\x200D';
    private int defaultCodePage;

    public ISCIIEncoding(int codePage)
      : base(codePage)
    {
      this.defaultCodePage = codePage - 57000;
      if (this.defaultCodePage < 2 || this.defaultCodePage > 11)
        throw new ArgumentException(Environment.GetResourceString("Argument_CodepageNotSupported", (object) codePage), "codePage");
    }

    internal ISCIIEncoding(SerializationInfo info, StreamingContext context)
      : base(0)
    {
      throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      this.SerializeEncoding(info, context);
      info.AddValue("m_maxByteSize", 2);
      info.SetType(typeof (MLangCodePageEncoding));
    }

    public override int GetMaxByteCount(int charCount)
    {
      if (charCount < 0)
        throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      long num1 = (long) charCount + 1L;
      if (this.EncoderFallback.MaxCharCount > 1)
        num1 *= (long) this.EncoderFallback.MaxCharCount;
      long num2 = num1 * 4L;
      if (num2 > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
      return (int) num2;
    }

    public override int GetMaxCharCount(int byteCount)
    {
      if (byteCount < 0)
        throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      long num = (long) byteCount + 1L;
      if (this.DecoderFallback.MaxCharCount > 1)
        num *= (long) this.DecoderFallback.MaxCharCount;
      if (num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_GetCharCountOverflow"));
      return (int) num;
    }

    [SecurityCritical]
    internal override unsafe int GetByteCount(char* chars, int count, EncoderNLS baseEncoder)
    {
      return this.GetBytes(chars, count, (byte*) null, 0, baseEncoder);
    }

    [SecurityCritical]
    internal override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS baseEncoder)
    {
      ISCIIEncoding.ISCIIEncoder isciiEncoder = (ISCIIEncoding.ISCIIEncoder) baseEncoder;
      Encoding.EncodingByteBuffer encodingByteBuffer = new Encoding.EncodingByteBuffer((Encoding) this, (EncoderNLS) isciiEncoder, bytes, byteCount, chars, charCount);
      int num1 = this.defaultCodePage;
      bool flag = false;
      if (isciiEncoder != null)
      {
        num1 = isciiEncoder.currentCodePage;
        flag = isciiEncoder.bLastVirama;
        if ((int) isciiEncoder.charLeftOver > 0)
        {
          encodingByteBuffer.Fallback(isciiEncoder.charLeftOver);
          flag = false;
        }
      }
      while (encodingByteBuffer.MoreData)
      {
        char nextChar = encodingByteBuffer.GetNextChar();
        if ((int) nextChar < 160)
        {
          if (encodingByteBuffer.AddByte((byte) nextChar))
            flag = false;
          else
            break;
        }
        else if ((int) nextChar < 2305 || (int) nextChar > 3439)
        {
          if (flag && ((int) nextChar == 8204 || (int) nextChar == 8205))
          {
            if ((int) nextChar == 8204)
            {
              if (!encodingByteBuffer.AddByte((byte) 232))
                break;
            }
            else if (!encodingByteBuffer.AddByte((byte) 233))
              break;
            flag = false;
          }
          else
          {
            encodingByteBuffer.Fallback(nextChar);
            flag = false;
          }
        }
        else
        {
          int num2 = ISCIIEncoding.UnicodeToIndicChar[(int) nextChar - 2305];
          byte b = (byte) num2;
          int num3 = 15 & num2 >> 8;
          int num4 = 61440 & num2;
          if (num2 == 0)
          {
            encodingByteBuffer.Fallback(nextChar);
            flag = false;
          }
          else
          {
            if (num3 != num1)
            {
              if (encodingByteBuffer.AddByte((byte) 239, (byte) (num3 | 64)))
                num1 = num3;
              else
                break;
            }
            if (encodingByteBuffer.AddByte(b, num4 != 0 ? 1 : 0))
            {
              flag = (int) b == 232;
              if (num4 != 0 && !encodingByteBuffer.AddByte(ISCIIEncoding.SecondIndicByte[num4 >> 12]))
                break;
            }
            else
              break;
          }
        }
      }
      if (num1 != this.defaultCodePage && (isciiEncoder == null || isciiEncoder.MustFlush))
      {
        if (encodingByteBuffer.AddByte((byte) 239, (byte) (this.defaultCodePage | 64)))
        {
          num1 = this.defaultCodePage;
        }
        else
        {
          int num2 = (int) encodingByteBuffer.GetNextChar();
        }
        flag = false;
      }
      if (isciiEncoder != null && (IntPtr) bytes != IntPtr.Zero)
      {
        if (!encodingByteBuffer.fallbackBuffer.bUsedEncoder)
          isciiEncoder.charLeftOver = char.MinValue;
        isciiEncoder.currentCodePage = num1;
        isciiEncoder.bLastVirama = flag;
        isciiEncoder.m_charsUsed = encodingByteBuffer.CharsUsed;
      }
      return encodingByteBuffer.Count;
    }

    [SecurityCritical]
    internal override unsafe int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
    {
      return this.GetChars(bytes, count, (char*) null, 0, baseDecoder);
    }

    [SecurityCritical]
    internal override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
    {
      ISCIIEncoding.ISCIIDecoder isciiDecoder = (ISCIIEncoding.ISCIIDecoder) baseDecoder;
      Encoding.EncodingCharBuffer encodingCharBuffer = new Encoding.EncodingCharBuffer((Encoding) this, (DecoderNLS) isciiDecoder, chars, charCount, bytes, byteCount);
      int index1 = this.defaultCodePage;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      char ch1 = char.MinValue;
      char ch2 = char.MinValue;
      if (isciiDecoder != null)
      {
        index1 = isciiDecoder.currentCodePage;
        flag1 = isciiDecoder.bLastATR;
        flag2 = isciiDecoder.bLastVirama;
        flag3 = isciiDecoder.bLastDevenagariStressAbbr;
        ch1 = isciiDecoder.cLastCharForNextNukta;
        ch2 = isciiDecoder.cLastCharForNoNextNukta;
      }
      bool flag4 = flag2 | flag1 | flag3 | (uint) ch1 > 0U;
      int index2 = -1;
      if (index1 >= 2 && index1 <= 11)
        index2 = ISCIIEncoding.IndicMappingIndex[index1];
      while (encodingCharBuffer.MoreData)
      {
        byte nextByte = encodingCharBuffer.GetNextByte();
        if (flag4)
        {
          flag4 = false;
          if (flag1)
          {
            if ((int) nextByte >= 66 && (int) nextByte <= 75)
            {
              index1 = (int) nextByte & 15;
              index2 = ISCIIEncoding.IndicMappingIndex[index1];
              flag1 = false;
              continue;
            }
            if ((int) nextByte == 64)
            {
              index1 = this.defaultCodePage;
              index2 = -1;
              if (index1 >= 2 && index1 <= 11)
                index2 = ISCIIEncoding.IndicMappingIndex[index1];
              flag1 = false;
              continue;
            }
            if ((int) nextByte == 65)
            {
              index1 = this.defaultCodePage;
              index2 = -1;
              if (index1 >= 2 && index1 <= 11)
                index2 = ISCIIEncoding.IndicMappingIndex[index1];
              flag1 = false;
              continue;
            }
            if (encodingCharBuffer.Fallback((byte) 239))
              flag1 = false;
            else
              break;
          }
          else if (flag2)
          {
            if ((int) nextByte == 232)
            {
              if (encodingCharBuffer.AddChar('\x200C'))
              {
                flag2 = false;
                continue;
              }
              break;
            }
            if ((int) nextByte == 233)
            {
              if (encodingCharBuffer.AddChar('\x200D'))
              {
                flag2 = false;
                continue;
              }
              break;
            }
            flag2 = false;
          }
          else if (flag3)
          {
            if ((int) nextByte == 184)
            {
              if (encodingCharBuffer.AddChar('॒'))
              {
                flag3 = false;
                continue;
              }
              break;
            }
            if ((int) nextByte == 191)
            {
              if (encodingCharBuffer.AddChar('॰'))
              {
                flag3 = false;
                continue;
              }
              break;
            }
            if (encodingCharBuffer.Fallback((byte) 240))
              flag3 = false;
            else
              break;
          }
          else
          {
            if ((int) nextByte == 233)
            {
              if (encodingCharBuffer.AddChar(ch1))
              {
                ch1 = ch2 = char.MinValue;
                continue;
              }
              break;
            }
            if (encodingCharBuffer.AddChar(ch2))
              ch1 = ch2 = char.MinValue;
            else
              break;
          }
        }
        if ((int) nextByte < 160)
        {
          if (!encodingCharBuffer.AddChar((char) nextByte))
            break;
        }
        else if ((int) nextByte == 239)
        {
          flag1 = flag4 = true;
        }
        else
        {
          char ch3 = ISCIIEncoding.IndicMapping[index2, 0, (int) nextByte - 160];
          char ch4 = ISCIIEncoding.IndicMapping[index2, 1, (int) nextByte - 160];
          if ((int) ch4 == 0 || (int) nextByte == 233)
          {
            if ((int) ch3 == 0)
            {
              if (!encodingCharBuffer.Fallback(nextByte))
                break;
            }
            else if (!encodingCharBuffer.AddChar(ch3))
              break;
          }
          else if ((int) nextByte == 232)
          {
            if (encodingCharBuffer.AddChar(ch3))
              flag2 = flag4 = true;
            else
              break;
          }
          else if (((int) ch4 & 61440) == 0)
          {
            flag4 = true;
            ch1 = ch4;
            ch2 = ch3;
          }
          else
            flag3 = flag4 = true;
        }
      }
      if (isciiDecoder == null || isciiDecoder.MustFlush)
      {
        if (flag1)
        {
          if (encodingCharBuffer.Fallback((byte) 239))
          {
            flag1 = false;
          }
          else
          {
            int num1 = (int) encodingCharBuffer.GetNextByte();
          }
        }
        else if (flag3)
        {
          if (encodingCharBuffer.Fallback((byte) 240))
          {
            flag3 = false;
          }
          else
          {
            int num2 = (int) encodingCharBuffer.GetNextByte();
          }
        }
        else if ((int) ch2 != 0)
        {
          if (encodingCharBuffer.AddChar(ch2))
          {
            ch2 = ch1 = char.MinValue;
          }
          else
          {
            int num3 = (int) encodingCharBuffer.GetNextByte();
          }
        }
      }
      if (isciiDecoder != null && (IntPtr) chars != IntPtr.Zero)
      {
        if (((!isciiDecoder.MustFlush ? 1 : ((uint) ch2 > 0U ? 1 : 0)) | (flag1 ? 1 : 0) | (flag3 ? 1 : 0)) != 0)
        {
          isciiDecoder.currentCodePage = index1;
          isciiDecoder.bLastVirama = flag2;
          isciiDecoder.bLastATR = flag1;
          isciiDecoder.bLastDevenagariStressAbbr = flag3;
          isciiDecoder.cLastCharForNextNukta = ch1;
          isciiDecoder.cLastCharForNoNextNukta = ch2;
        }
        else
        {
          isciiDecoder.currentCodePage = this.defaultCodePage;
          isciiDecoder.bLastVirama = false;
          isciiDecoder.bLastATR = false;
          isciiDecoder.bLastDevenagariStressAbbr = false;
          isciiDecoder.cLastCharForNextNukta = char.MinValue;
          isciiDecoder.cLastCharForNoNextNukta = char.MinValue;
        }
        isciiDecoder.m_bytesUsed = encodingCharBuffer.BytesUsed;
      }
      return encodingCharBuffer.Count;
    }

    public override Decoder GetDecoder()
    {
      return (Decoder) new ISCIIEncoding.ISCIIDecoder((Encoding) this);
    }

    public override Encoder GetEncoder()
    {
      return (Encoder) new ISCIIEncoding.ISCIIEncoder((Encoding) this);
    }

    public override int GetHashCode()
    {
      return this.defaultCodePage + this.EncoderFallback.GetHashCode() + this.DecoderFallback.GetHashCode();
    }

    [Serializable]
    internal class ISCIIEncoder : EncoderNLS
    {
      internal int defaultCodePage;
      internal int currentCodePage;
      internal bool bLastVirama;

      internal override bool HasState
      {
        get
        {
          if ((int) this.charLeftOver == 0)
            return this.currentCodePage != this.defaultCodePage;
          return true;
        }
      }

      public ISCIIEncoder(Encoding encoding)
        : base(encoding)
      {
        this.currentCodePage = this.defaultCodePage = encoding.CodePage - 57000;
      }

      public override void Reset()
      {
        this.bLastVirama = false;
        this.charLeftOver = char.MinValue;
        if (this.m_fallbackBuffer == null)
          return;
        this.m_fallbackBuffer.Reset();
      }
    }

    [Serializable]
    internal class ISCIIDecoder : DecoderNLS
    {
      internal int currentCodePage;
      internal bool bLastATR;
      internal bool bLastVirama;
      internal bool bLastDevenagariStressAbbr;
      internal char cLastCharForNextNukta;
      internal char cLastCharForNoNextNukta;

      internal override bool HasState
      {
        get
        {
          if ((int) this.cLastCharForNextNukta == 0 && (int) this.cLastCharForNoNextNukta == 0 && !this.bLastATR)
            return this.bLastDevenagariStressAbbr;
          return true;
        }
      }

      public ISCIIDecoder(Encoding encoding)
        : base(encoding)
      {
        this.currentCodePage = encoding.CodePage - 57000;
      }

      public override void Reset()
      {
        this.bLastATR = false;
        this.bLastVirama = false;
        this.bLastDevenagariStressAbbr = false;
        this.cLastCharForNextNukta = char.MinValue;
        this.cLastCharForNoNextNukta = char.MinValue;
        if (this.m_fallbackBuffer == null)
          return;
        this.m_fallbackBuffer.Reset();
      }
    }
  }
}
