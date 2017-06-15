// Decompiled with JetBrains decompiler
// Type: System.Text.Normalization
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Text
{
  internal class Normalization
  {
    private static volatile bool NFC;
    private static volatile bool NFD;
    private static volatile bool NFKC;
    private static volatile bool NFKD;
    private static volatile bool IDNA;
    private static volatile bool NFCDisallowUnassigned;
    private static volatile bool NFDDisallowUnassigned;
    private static volatile bool NFKCDisallowUnassigned;
    private static volatile bool NFKDDisallowUnassigned;
    private static volatile bool IDNADisallowUnassigned;
    private static volatile bool Other;
    private const int ERROR_SUCCESS = 0;
    private const int ERROR_NOT_ENOUGH_MEMORY = 8;
    private const int ERROR_INVALID_PARAMETER = 87;
    private const int ERROR_INSUFFICIENT_BUFFER = 122;
    private const int ERROR_NO_UNICODE_TRANSLATION = 1113;

    [SecurityCritical]
    private static unsafe void InitializeForm(NormalizationForm form, string strDataFile)
    {
      byte* pTableData = (byte*) null;
      if (!Environment.IsWindows8OrAbove)
      {
        if (strDataFile == null)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNormalizationForm"));
        pTableData = GlobalizationAssembly.GetGlobalizationResourceBytePtr(typeof (Normalization).Assembly, strDataFile);
        if ((IntPtr) pTableData == IntPtr.Zero)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNormalizationForm"));
      }
      Normalization.nativeNormalizationInitNormalization(form, pTableData);
    }

    [SecurityCritical]
    private static void EnsureInitialized(NormalizationForm form)
    {
      switch ((ExtendedNormalizationForms) form)
      {
        case ExtendedNormalizationForms.FormCDisallowUnassigned:
          if (Normalization.NFCDisallowUnassigned)
            break;
          Normalization.InitializeForm(form, "normnfc.nlp");
          Normalization.NFCDisallowUnassigned = true;
          break;
        case ExtendedNormalizationForms.FormDDisallowUnassigned:
          if (Normalization.NFDDisallowUnassigned)
            break;
          Normalization.InitializeForm(form, "normnfd.nlp");
          Normalization.NFDDisallowUnassigned = true;
          break;
        case ExtendedNormalizationForms.FormKCDisallowUnassigned:
          if (Normalization.NFKCDisallowUnassigned)
            break;
          Normalization.InitializeForm(form, "normnfkc.nlp");
          Normalization.NFKCDisallowUnassigned = true;
          break;
        case ExtendedNormalizationForms.FormKDDisallowUnassigned:
          if (Normalization.NFKDDisallowUnassigned)
            break;
          Normalization.InitializeForm(form, "normnfkd.nlp");
          Normalization.NFKDDisallowUnassigned = true;
          break;
        case ExtendedNormalizationForms.FormIdnaDisallowUnassigned:
          if (Normalization.IDNADisallowUnassigned)
            break;
          Normalization.InitializeForm(form, "normidna.nlp");
          Normalization.IDNADisallowUnassigned = true;
          break;
        case ExtendedNormalizationForms.FormC:
          if (Normalization.NFC)
            break;
          Normalization.InitializeForm(form, "normnfc.nlp");
          Normalization.NFC = true;
          break;
        case ExtendedNormalizationForms.FormD:
          if (Normalization.NFD)
            break;
          Normalization.InitializeForm(form, "normnfd.nlp");
          Normalization.NFD = true;
          break;
        case ExtendedNormalizationForms.FormKC:
          if (Normalization.NFKC)
            break;
          Normalization.InitializeForm(form, "normnfkc.nlp");
          Normalization.NFKC = true;
          break;
        case ExtendedNormalizationForms.FormKD:
          if (Normalization.NFKD)
            break;
          Normalization.InitializeForm(form, "normnfkd.nlp");
          Normalization.NFKD = true;
          break;
        case ExtendedNormalizationForms.FormIdna:
          if (Normalization.IDNA)
            break;
          Normalization.InitializeForm(form, "normidna.nlp");
          Normalization.IDNA = true;
          break;
        default:
          if (Normalization.Other)
            break;
          Normalization.InitializeForm(form, (string) null);
          Normalization.Other = true;
          break;
      }
    }

    [SecurityCritical]
    internal static bool IsNormalized(string strInput, NormalizationForm normForm)
    {
      Normalization.EnsureInitialized(normForm);
      int num1 = 0;
      int num2 = (int) normForm;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      int& iError = @num1;
      string lpString = strInput;
      int length = lpString.Length;
      bool flag = Normalization.nativeNormalizationIsNormalizedString((NormalizationForm) num2, iError, lpString, length);
      if (num1 <= 8)
      {
        if (num1 == 0)
          return flag;
        if (num1 == 8)
          throw new OutOfMemoryException(Environment.GetResourceString("Arg_OutOfMemoryException"));
      }
      else if (num1 == 87 || num1 == 1113)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"), "strInput");
      throw new InvalidOperationException(Environment.GetResourceString("UnknownError_Num", (object) num1));
    }

    [SecurityCritical]
    internal static string Normalize(string strInput, NormalizationForm normForm)
    {
      Normalization.EnsureInitialized(normForm);
      int num1 = 0;
      int num2 = (int) normForm;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      int& iError1 = @num1;
      string lpSrcString1 = strInput;
      int length1 = lpSrcString1.Length;
      // ISSUE: variable of the null type
      __Null local = null;
      int cwDstLength = 0;
      int length2 = Normalization.nativeNormalizationNormalizeString((NormalizationForm) num2, iError1, lpSrcString1, length1, (char[]) local, cwDstLength);
      if (num1 != 0)
      {
        if (num1 == 87)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"), "strInput");
        if (num1 == 8)
          throw new OutOfMemoryException(Environment.GetResourceString("Arg_OutOfMemoryException"));
        throw new InvalidOperationException(Environment.GetResourceString("UnknownError_Num", (object) num1));
      }
      if (length2 == 0)
        return string.Empty;
      char[] chArray;
      do
      {
        chArray = new char[length2];
        int num3 = (int) normForm;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        int& iError2 = @num1;
        string lpSrcString2 = strInput;
        int length3 = lpSrcString2.Length;
        char[] lpDstString = chArray;
        int length4 = lpDstString.Length;
        length2 = Normalization.nativeNormalizationNormalizeString((NormalizationForm) num3, iError2, lpSrcString2, length3, lpDstString, length4);
        if (num1 != 0)
        {
          if (num1 <= 87)
          {
            if (num1 != 8)
            {
              if (num1 == 87)
                goto label_15;
              else
                goto label_17;
            }
            else
              goto label_16;
          }
        }
        else
          goto label_18;
      }
      while (num1 == 122);
      if (num1 != 1113)
        goto label_17;
label_15:
      throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequence", (object) length2), "strInput");
label_16:
      throw new OutOfMemoryException(Environment.GetResourceString("Arg_OutOfMemoryException"));
label_17:
      throw new InvalidOperationException(Environment.GetResourceString("UnknownError_Num", (object) num1));
label_18:
      return new string(chArray, 0, length2);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int nativeNormalizationNormalizeString(NormalizationForm normForm, ref int iError, string lpSrcString, int cwSrcLength, char[] lpDstString, int cwDstLength);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool nativeNormalizationIsNormalizedString(NormalizationForm normForm, ref int iError, string lpString, int cwLength);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern unsafe void nativeNormalizationInitNormalization(NormalizationForm normForm, byte* pTableData);
  }
}
