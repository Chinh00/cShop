import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  /* config options here */
    eslint: {
        ignoreDuringBuilds: true, 
    },
    pageExtensions: ['ts', 'tsx'],
    images: {
        domains: ["lh3.googleusercontent.com"],
    },
};

export default nextConfig;
