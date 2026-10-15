import "@/styles/globals.css"
import { Inter } from "next/font/google"
import { Providers } from "@/lib/providers"
import type React from "react" // Added import for React

const inter = Inter({ subsets: ["latin"] })

export const metadata = {
  title: "Pizza Store Enterprise",
  description: "An enterprise-grade pizza ordering system",
}

export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <html lang="en">
      <body className={inter.className}>
        <Providers>{children}</Providers>
      </body>
    </html>
  )
}



import './globals.css'