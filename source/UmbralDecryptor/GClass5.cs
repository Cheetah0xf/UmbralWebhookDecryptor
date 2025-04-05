using System;
using System.Runtime.InteropServices;

// Token: 0x02000109 RID: 265
public static class GClass5
{
    // Token: 0x06000400 RID: 1024
    [DllImport("bcrypt.dll")]
    public static extern uint BCryptOpenAlgorithmProvider(out IntPtr phAlgorithm, [MarshalAs(UnmanagedType.LPWStr)] string pszAlgId, [MarshalAs(UnmanagedType.LPWStr)] string pszImplementation, uint dwFlags);

    // Token: 0x06000401 RID: 1025
    [DllImport("bcrypt.dll")]
    public static extern uint BCryptCloseAlgorithmProvider(IntPtr hAlgorithm, uint flags);

    // Token: 0x06000402 RID: 1026
    [DllImport("bcrypt.dll")]
    public static extern uint BCryptGetProperty(IntPtr hObject, [MarshalAs(UnmanagedType.LPWStr)] string pszProperty, byte[] pbOutput, int cbOutput, ref int pcbResult, uint flags);

    // Token: 0x06000403 RID: 1027
    [DllImport("bcrypt.dll")]
    internal static extern uint BCryptSetProperty(IntPtr hObject, [MarshalAs(UnmanagedType.LPWStr)] string pszProperty, byte[] pbInput, int cbInput, int dwFlags);

    // Token: 0x06000404 RID: 1028
    [DllImport("bcrypt.dll")]
    public static extern uint BCryptImportKey(IntPtr hAlgorithm, IntPtr hImportKey, [MarshalAs(UnmanagedType.LPWStr)] string pszBlobType, out IntPtr phKey, IntPtr pbKeyObject, int cbKeyObject, byte[] pbInput, int cbInput, uint dwFlags);

    // Token: 0x06000405 RID: 1029
    [DllImport("bcrypt.dll")]
    public static extern uint BCryptDestroyKey(IntPtr hKey);

    [DllImport("bcrypt.dll")]
    internal static extern uint BCryptDecrypt(IntPtr hKey, byte[] pbInput, int cbInput, ref GClass5.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO pPaddingInfo, byte[] pbIV, int cbIV, byte[] pbOutput, int cbOutput, ref int pcbResult, int dwFlags);

    // Token: 0x0400054B RID: 1355
    public const uint uint_0 = 0U;

    // Token: 0x0400054C RID: 1356
    public const uint uint_1 = 8U;

    // Token: 0x0400054D RID: 1357
    public const uint uint_2 = 4U;

    // Token: 0x0400054E RID: 1358
    public static readonly byte[] byte_0 = BitConverter.GetBytes(1296188491);

    // Token: 0x0400054F RID: 1359
    public static readonly string string_0 = "ObjectLength";

    // Token: 0x04000550 RID: 1360
    public static readonly string string_1 = "ChainingModeGCM";

    // Token: 0x04000551 RID: 1361
    public static readonly string string_2 = "AuthTagLength";

    // Token: 0x04000552 RID: 1362
    public static readonly string string_3 = "ChainingMode";

    // Token: 0x04000553 RID: 1363
    public static readonly string string_4 = "KeyDataBlob";

    // Token: 0x04000554 RID: 1364
    public static readonly string string_5 = "AES";

    // Token: 0x04000555 RID: 1365
    public static readonly string string_6 = "Microsoft Primitive Provider";

    // Token: 0x04000556 RID: 1366
    public static readonly int int_0 = 1;

    // Token: 0x04000557 RID: 1367
    public static readonly int int_1 = 1;

    // Token: 0x04000558 RID: 1368
    public static readonly uint uint_3 = 3221266434U;

    // Token: 0x0200010A RID: 266
    public struct BCRYPT_PSS_PADDING_INFO
    {
        // Token: 0x06000409 RID: 1033 RVA: 0x000039C0 File Offset: 0x00001BC0
        public BCRYPT_PSS_PADDING_INFO(string pszAlgId, int cbSalt)
        {
            this.pszAlgId = pszAlgId;
            this.cbSalt = cbSalt;
        }

        // Token: 0x04000559 RID: 1369
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pszAlgId;

        // Token: 0x0400055A RID: 1370
        public int cbSalt;
    }

    // Token: 0x0200010B RID: 267
    public struct BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO : IDisposable
    {
        // Token: 0x0600040A RID: 1034 RVA: 0x0001D820 File Offset: 0x0001BA20
        public BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO(byte[] iv, byte[] aad, byte[] tag)
        {
            this = default(GClass5.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO);
            this.dwInfoVersion = GClass5.int_1;
            this.cbSize = Marshal.SizeOf(typeof(GClass5.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO));
            if (iv != null)
            {
                this.cbNonce = iv.Length;
                this.pbNonce = Marshal.AllocHGlobal(this.cbNonce);
                Marshal.Copy(iv, 0, this.pbNonce, this.cbNonce);
            }
            if (aad != null)
            {
                this.cbAuthData = aad.Length;
                this.pbAuthData = Marshal.AllocHGlobal(this.cbAuthData);
                Marshal.Copy(aad, 0, this.pbAuthData, this.cbAuthData);
            }
            if (tag != null)
            {
                this.cbTag = tag.Length;
                this.pbTag = Marshal.AllocHGlobal(this.cbTag);
                Marshal.Copy(tag, 0, this.pbTag, this.cbTag);
                this.cbMacContext = tag.Length;
                this.pbMacContext = Marshal.AllocHGlobal(this.cbMacContext);
            }
        }

        // Token: 0x0600040B RID: 1035 RVA: 0x0001D900 File Offset: 0x0001BB00
        public void Dispose()
        {
            if (this.pbNonce != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(this.pbNonce);
            }
            if (this.pbTag != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(this.pbTag);
            }
            if (this.pbAuthData != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(this.pbAuthData);
            }
            if (this.pbMacContext != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(this.pbMacContext);
            }
        }

        // Token: 0x0400055B RID: 1371
        public int cbSize;

        // Token: 0x0400055C RID: 1372
        public int dwInfoVersion;

        // Token: 0x0400055D RID: 1373
        public IntPtr pbNonce;

        // Token: 0x0400055E RID: 1374
        public int cbNonce;

        // Token: 0x0400055F RID: 1375
        public IntPtr pbAuthData;

        // Token: 0x04000560 RID: 1376
        public int cbAuthData;

        // Token: 0x04000561 RID: 1377
        public IntPtr pbTag;

        // Token: 0x04000562 RID: 1378
        public int cbTag;

        // Token: 0x04000563 RID: 1379
        public IntPtr pbMacContext;

        // Token: 0x04000564 RID: 1380
        public int cbMacContext;

        // Token: 0x04000565 RID: 1381
        public int cbAAD;

        // Token: 0x04000566 RID: 1382
        public long cbData;

        // Token: 0x04000567 RID: 1383
        public int dwFlags;
    }

    // Token: 0x0200010C RID: 268
    public struct BCRYPT_KEY_LENGTHS_STRUCT
    {
        // Token: 0x04000568 RID: 1384
        public int dwMinLength;

        // Token: 0x04000569 RID: 1385
        public int dwMaxLength;

        // Token: 0x0400056A RID: 1386
        public int dwIncrement;
    }

    // Token: 0x0200010D RID: 269
    public struct BCRYPT_OAEP_PADDING_INFO
    {
        // Token: 0x0600040C RID: 1036 RVA: 0x000039D0 File Offset: 0x00001BD0
        public BCRYPT_OAEP_PADDING_INFO(string alg)
        {
            this.pszAlgId = alg;
            this.pbLabel = IntPtr.Zero;
            this.cbLabel = 0;
        }

        // Token: 0x0400056B RID: 1387
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pszAlgId;

        // Token: 0x0400056C RID: 1388
        public IntPtr pbLabel;

        // Token: 0x0400056D RID: 1389
        public int cbLabel;
    }
}
