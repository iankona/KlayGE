/**
 * @file D3D12InterfaceLoader.hpp
 * @author Minmin Gong
 *
 * @section DESCRIPTION
 *
 * This source file is part of KlayGE
 * For the latest info, see http://www.klayge.org
 *
 * @section LICENSE
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published
 * by the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
 *
 * You may alternatively use this source under the terms of
 * the KlayGE Proprietary License (KPL). You can obtained such a license
 * from http://www.klayge.org/licensing/.
 */

#ifndef _D3D12INTERFACELOADER_HPP
#define _D3D12INTERFACELOADER_HPP

#pragma once

#include <KlayGE/SALWrapper.hpp>
#include <KlayGE/D3D12/D3D12Typedefs.hpp>

namespace KlayGE
{
    class D3D12InterfaceLoader
    {
    public:
        static D3D12InterfaceLoader& Instance();

        HRESULT CreateDXGIFactory1(REFIID riid, void** ppFactory) const;
        HRESULT D3D12CreateDevice(IUnknown* pAdapter,
            D3D_FEATURE_LEVEL MinimumFeatureLevel, REFIID riid,
            void** ppDevice);
        HRESULT D3D12GetDebugInterface(REFIID riid, void** ppvDebug) const;
        HRESULT D3D12SerializeRootSignature(D3D12_ROOT_SIGNATURE_DESC const * pRootSignature,
            D3D_ROOT_SIGNATURE_VERSION Version, ID3DBlob** ppBlob, ID3DBlob** ppErrorBlob) const;
#ifdef KLAYGE_PLATFORM_WINDOWS_DESKTOP
        HRESULT D3DCompile(LPCVOID pSrcData, SIZE_T SrcDataSize, LPCSTR pSourceName,
            D3D_SHADER_MACRO const * pDefines, ID3DInclude* pInclude, LPCSTR pEntrypoint,
            LPCSTR pTarget, UINT Flags1, UINT Flags2, ID3DBlob** ppCode, ID3DBlob** ppErrorMsgs) const;
        HRESULT D3DReflect(LPCVOID pSrcData, SIZE_T SrcDataSize, REFIID pInterface, void** ppReflector) const;
        HRESULT D3DStripShader(LPCVOID pShaderBytecode, SIZE_T BytecodeLength, UINT uStripFlags, ID3DBlob** ppStrippedBlob) const;
#endif

    private:
        D3D12InterfaceLoader();
        ~D3D12InterfaceLoader();

    private:
        typedef HRESULT (WINAPI *CreateDXGIFactory1Func)(REFIID riid, void** ppFactory);
        typedef HRESULT (WINAPI *D3D12CreateDeviceFunc)(IUnknown* pAdapter,
            D3D_FEATURE_LEVEL MinimumFeatureLevel, REFIID riid,
            void** ppDevice);
        typedef HRESULT (WINAPI *D3D12GetDebugInterfaceFunc)(REFIID riid, void** ppvDebug);
        typedef HRESULT (WINAPI *D3D12SerializeRootSignatureFunc)(D3D12_ROOT_SIGNATURE_DESC const * pRootSignature,
            D3D_ROOT_SIGNATURE_VERSION Version, ID3DBlob** ppBlob, ID3DBlob** ppErrorBlob);

        CreateDXGIFactory1Func DynamicCreateDXGIFactory1_;
        D3D12CreateDeviceFunc DynamicD3D12CreateDevice_;
        D3D12GetDebugInterfaceFunc DynamicD3D12GetDebugInterface_;
        D3D12SerializeRootSignatureFunc DynamicD3D12SerializeRootSignature_;

#ifdef KLAYGE_PLATFORM_WINDOWS_DESKTOP
        HMODULE mod_dxgi_;
        HMODULE mod_d3d12_;
        HMODULE mod_d3dcompiler_;

        typedef HRESULT (WINAPI *D3DCompileFunc)(LPCVOID pSrcData, SIZE_T SrcDataSize, LPCSTR pSourceName,
            D3D_SHADER_MACRO const * pDefines, ID3DInclude* pInclude, LPCSTR pEntrypoint,
            LPCSTR pTarget, UINT Flags1, UINT Flags2, ID3DBlob** ppCode, ID3DBlob** ppErrorMsgs);
        typedef HRESULT (WINAPI *D3DReflectFunc)(LPCVOID pSrcData, SIZE_T SrcDataSize, REFIID pInterface, void** ppReflector);
        typedef HRESULT (WINAPI *D3DStripShaderFunc)(LPCVOID pShaderBytecode, SIZE_T BytecodeLength, UINT uStripFlags, ID3DBlob** ppStrippedBlob);

        D3DCompileFunc DynamicD3DCompile_;
        D3DReflectFunc DynamicD3DReflect_;
        D3DStripShaderFunc DynamicD3DStripShader_;
#endif
    };
}

#endif			// _D3D12INTERFACELOADER_HPP
