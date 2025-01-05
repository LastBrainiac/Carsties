import { numberWithCommas } from "@/lib/numberWithComma";
import { Auction, AuctionFinished } from "@/types";
import Image from "next/image";
import Link from "next/link";
import React from "react";

type Props = {
  finishedAuction: AuctionFinished;
  auction: Auction;
};

export default function AuctionFinishedToast({ auction, finishedAuction }: Props) {
  return (
    <Link href={`/auctions/details/${auction.id}`} className="flex flex-col items-center">
        <div className="flex flex-row items-center gap-2">  
            <Image 
                src={auction.imageUrl}
                alt={auction.model}
                height={120}
                width={120}
                className="rounded-lg w-auto h-auto"
            />     
            <div className="flex flex-col">
                <span>Auction for {auction.make} {auction.model} has finished!</span>
                {finishedAuction.itemSold && finishedAuction.amount ? (
                    <span>{finishedAuction.winner} won the auction for $${numberWithCommas(finishedAuction.amount)}</span>
                ) : (
                    <span>The auction did not meet the reserve price</span>
                )}
            </div>                 
        </div>
    </Link>
  )
}
