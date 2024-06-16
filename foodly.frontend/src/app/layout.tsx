import type { Metadata } from 'next'
import { Inter } from 'next/font/google'
import './globals.css'
import { RQProvider } from './_components/utility_components/RQProvider'

const inter = Inter({ subsets: ['latin'] })

export const metadata: Metadata = {
  title: 'Create Next App',
  description: 'Generated by create next app',
}

export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <html lang="en">
      <body className={inter.className}>
        <RQProvider>
          {children}

        </RQProvider>
      </body>
    </html>
  )
}
