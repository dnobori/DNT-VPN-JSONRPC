
#pragma warning disable CA2235 // Mark all non-serializable fields

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.CompilerServices;

using System.Net.Sockets;

using UINT64 = System.UInt64;
using UINT = System.UInt32;
using UINT32 = System.UInt32;
using INT = System.Int32;
using INT32 = System.Int32;
using INT64 = System.Int64;
using time_64t = System.Int64;
using UINT_PTR = System.Int32;
using LONG_PTR = System.Int64;
using WORD = System.UInt16;
using USHORT = System.UInt16;
using SHORT = System.Int16;
using BYTE = System.Byte;
using UCHAR = System.Byte;
using CHAR = System.SByte;

public unsafe static class Helper_SHA0HashPassword
{
    public static class FromCHelper
    {
        [MethodImpl]
        public static UINT rol(UINT bits, UINT value) => (((value) << ((INT)bits)) | ((value) >> (32 - ((INT)bits))));

        public const int MY_SHA0_DIGEST_SIZE = 20;
    }

    public unsafe ref struct MY_SHA0_CTX
    {
        public UINT64 count;
        public fixed UCHAR buf[64];
        public fixed UINT state[8];  // upto SHA2
    }
    
    public static void MY_SHA0_Transform(MY_SHA0_CTX* ctx)
    {
        Span<UINT> W = stackalloc UINT[80];
        UINT A, B, C, D, E;
        UCHAR* p = ctx->buf;
        int t;
        for (t = 0; t < 16; ++t)
        {
            UINT tmp = (UINT)(*p++) << 24;
            tmp |= (UINT)(*p++) << 16;
            tmp |= (UINT)(*p++) << 8;
            tmp |= (UINT)(*p++);
            W[t] = tmp;
        }
        for (; t < 80; t++)
        {
            //W[t] = rol(1,W[t-3] ^ W[t-8] ^ W[t-14] ^ W[t-16]);
            W[t] = (W[t - 3] ^ W[t - 8] ^ W[t - 14] ^ W[t - 16]);
        }
        A = ctx->state[0];
        B = ctx->state[1];
        C = ctx->state[2];
        D = ctx->state[3];
        E = ctx->state[4];
        for (t = 0; t < 80; t++)
        {
            UINT tmp = FromCHelper.rol(5, A) + E + W[t];
            if (t < 20)
                tmp += (D ^ (B & (C ^ D))) + 0x5A827999;
            else if (t < 40)
                tmp += (B ^ C ^ D) + 0x6ED9EBA1;
            else if (t < 60)
                tmp += ((B & C) | (D & (B | C))) + 0x8F1BBCDC;
            else
                tmp += (B ^ C ^ D) + 0xCA62C1D6;
            E = D;
            D = C;
            C = FromCHelper.rol(30, B);
            B = A;
            A = tmp;
        }
        ctx->state[0] += A;
        ctx->state[1] += B;
        ctx->state[2] += C;
        ctx->state[3] += D;
        ctx->state[4] += E;
    }

    public static void MY_SHA0_init(MY_SHA0_CTX* ctx)
    {
        //ctx->f = &SHA_VTAB;
        ctx->state[0] = 0x67452301;
        ctx->state[1] = 0xEFCDAB89;
        ctx->state[2] = 0x98BADCFE;
        ctx->state[3] = 0x10325476;
        ctx->state[4] = 0xC3D2E1F0;
        ctx->count = 0;
    }

    public static void MY_SHA0_update(MY_SHA0_CTX* ctx, ReadOnlySpan<byte> data)
    {
        UINT len = (UINT)data.Length;
        int i = (int)(ctx->count & 63);
        fixed (UCHAR* ptr = data)
        {
            UCHAR* p = ptr;
            ctx->count += len;
            while (len-- != 0)
            {
                ctx->buf[i++] = *p++;
                if (i == 64)
                {
                    MY_SHA0_Transform(ctx);
                    i = 0;
                }
            }
        }
    }

    public static void MY_SHA0_final(MY_SHA0_CTX* ctx)
    {
        UCHAR* p = ctx->buf;
        UINT64 cnt = ctx->count * 8;
        int i;
        var x = new byte[1] { 0x80 };
        MY_SHA0_update(ctx, x);
        var zero = new byte[1] { 0x00 };
        while ((ctx->count & 63) != 56)
        {
            MY_SHA0_update(ctx, zero);
        }
        Span<byte> t = new byte[1];
        for (i = 0; i < 8; ++i)
        {
            t[0] = (UCHAR)(cnt >> ((7 - i) * 8));
            MY_SHA0_update(ctx, t);
        }
        for (i = 0; i < 5; i++)
        {
            UINT tmp = ctx->state[i];
            *p++ = (BYTE)(tmp >> 24);
            *p++ = (BYTE)(tmp >> 16);
            *p++ = (BYTE)(tmp >> 8);
            *p++ = (BYTE)(tmp >> 0);
        }
    }

    public static byte[] MY_SHA0_hash(ReadOnlySpan<byte> data)
    {
        MY_SHA0_CTX ctx;
        MY_SHA0_init(&ctx);
        MY_SHA0_update(&ctx, data);
        MY_SHA0_final(&ctx);

        Span<byte> tmp = new Span<BYTE>(ctx.buf, FromCHelper.MY_SHA0_DIGEST_SIZE);

        return tmp.ToArray();
    }

    public static byte[] Internal_SHA0(ReadOnlySpan<byte> data)
        => MY_SHA0_hash(data);

    public static byte[] HashSHA0(ReadOnlySpan<byte> src)
    {
        return Helper_SHA0HashPassword.Internal_SHA0(src);
    }

    public static byte[] HashSHA0Password(string username, string password)
    {
        byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
        byte[] usernameBytes = Encoding.ASCII.GetBytes(username.ToUpperInvariant());

        MemoryStream ms = new MemoryStream();
        ms.Write(passwordBytes, 0, passwordBytes.Length);
        ms.Write(usernameBytes, 0, usernameBytes.Length);

        return HashSHA0(ms.ToArray());
    }
}

