#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Interop;

#endregion

namespace GraemeF.FetchLinks
{
    public class Tidy
    {
        public Tidy()
        {

        }

//mports System.Text
//Imports System.Runtime.InteropServices
//
//Module Main
//    Private Structure TidyBuffer
//        Dim bp As IntPtr
//        Dim size As Integer
//        Dim allocated As Integer
//        Dim offset As Integer
//    End Structure
//    Private Declare Function tidyCreate Lib "libtidy.dll" () As Integer
//    Private Declare Function tidyBufAlloc Lib "libtidy.dll" (ByRef Buffer As TidyBuffer, ByVal allocSize As Integer) As Integer
//    Private Declare Function tidyBufFree Lib "libtidy.dll" (ByRef Buffer As TidyBuffer) As Integer
//    Private Declare Function tidyOptSetBool Lib "libtidy.dll" (ByVal hDoc As Integer, ByVal OptID As Integer, ByVal Value As Integer) As Integer
//    Private Declare Function tidySetErrorBuffer Lib "libtidy.dll" (ByVal hDoc As Integer, ByRef Buffer As TidyBuffer) As Integer
//    Private Declare Ansi Function tidyParseString Lib "libtidy.dll" (ByVal hDoc As Integer, ByVal Input As String) As Integer
//    Private Declare Function tidyCleanAndRepair Lib "libtidy.dll" (ByVal hDoc As Integer) As Integer
//    Private Declare Function tidySaveBuffer Lib "libtidy.dll" (ByVal hDoc As Integer, ByRef Buffer As TidyBuffer) As Integer
//    Private Declare Function tidyRelease Lib "libtidy.dll" (ByVal hDoc As Integer) As Integer
//    Sub Main()
//        Dim iResult As Integer
//        Dim outBuf As TidyBuffer
//        Dim errBuf As TidyBuffer
//        Dim sErr As String
//
//        Dim hDoc As Integer = tidyCreate()
//        iResult = tidyBufAlloc(errBuf, 4096)
//        iResult = tidySetErrorBuffer(hDoc, errBuf)
//        iResult = tidyOptSetBool(hDoc, 23, 1)   ' TidyXhtmlOut
//
//        Dim Input As String = "<div class=subhead>Subhead</div><p>Paragraph without closing tag.<p>Second paragraph.</p>"
//        If CBool(tidyParseString(hDoc, Input)) Then
//            sErr = Marshal.PtrToStringAnsi(errBuf.bp)
//        End If
//
//        If CBool(tidyCleanAndRepair(hDoc)) Then
//            sErr = Marshal.PtrToStringAnsi(errBuf.bp)
//        End If
//        iResult = tidyBufAlloc(outBuf, 16384)
//        If CBool(tidySaveBuffer(hDoc, outBuf)) Then
//            sErr = Marshal.PtrToStringAnsi(errBuf.bp)
//        End If
//        Dim sOutput As String = Marshal.PtrToStringAnsi(outBuf.bp)
//
//        tidyBufFree(outBuf)
//        tidyBufFree(errBuf)
//        tidyRelease(hDoc)
//    End Sub
//End Module
    }
}
