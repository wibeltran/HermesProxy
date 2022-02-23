﻿using Framework.Constants;
using HermesProxy.Enums;
using HermesProxy.World;
using HermesProxy.World.Enums;
using HermesProxy.World.Objects;
using HermesProxy.World.Server.Packets;
using System;

namespace HermesProxy.World.Server
{
    public partial class WorldSocket
    {
        // Handlers for CMSG opcodes coming from the modern client
        [PacketHandler(Opcode.CMSG_BANKER_ACTIVATE)]
        [PacketHandler(Opcode.CMSG_BINDER_ACTIVATE)]
        [PacketHandler(Opcode.CMSG_LIST_INVENTORY)]
        [PacketHandler(Opcode.CMSG_SPIRIT_HEALER_ACTIVATE)]
        [PacketHandler(Opcode.CMSG_TALK_TO_GOSSIP)]
        [PacketHandler(Opcode.CMSG_TRAINER_LIST)]
        void HandleInteractWithNPC(InteractWithNPC interact)
        {
            WorldPacket packet = new WorldPacket(interact.GetUniversalOpcode());
            packet.WriteGuid(interact.CreatureGUID.To64());
            SendPacketToServer(packet);
        }

        [PacketHandler(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        void HandleGossipSelectOption(GossipSelectOption gossip)
        {
            WorldPacket packet = new WorldPacket(Opcode.CMSG_GOSSIP_SELECT_OPTION);
            packet.WriteGuid(gossip.GossipUnit.To64());
            if (LegacyVersion.AddedInVersion(ClientVersionBuild.V2_0_1_6180))
                packet.WriteUInt32(gossip.GossipID);
            packet.WriteUInt32(gossip.GossipIndex);
            if (!String.IsNullOrEmpty(gossip.PromotionCode))
                packet.WriteCString(gossip.PromotionCode);
            SendPacketToServer(packet);
        }

        [PacketHandler(Opcode.CMSG_BUY_BANK_SLOT)]
        void HandleBuyBankSlot(BuyBankSlot bank)
        {
            WorldPacket packet = new WorldPacket(Opcode.CMSG_BUY_BANK_SLOT);
            packet.WriteGuid(bank.Guid.To64());
            SendPacketToServer(packet);
        }
    }
}
