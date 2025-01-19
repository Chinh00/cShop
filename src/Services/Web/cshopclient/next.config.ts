import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  /* config options here */
    eslint: {
        ignoreDuringBuilds: true, 
    },
    pageExtensions: ['ts', 'tsx']
};

export default nextConfig;
