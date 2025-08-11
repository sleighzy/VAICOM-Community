-- VAICOM PRO server-side script
-- speech.lua (append)
-- www.vaicompro.com

function make(message)
    -- Handle Morse code messages separately
    if message.type == base.Message.type.TYPE_MORZE then
        return p.morze(message.string, message.parameters.type)
    end

    -- Sanity check: ensure sender exists
    base.assert(message.sender ~= nil)

    -- Determine speech protocol based on sender's country
    local country = getCountry(message)
    local stateProtocol = protocolByCountry[country] or defaultProtocol or 'common'
    local protocol = protocols[stateProtocol]
    base.assert(protocol ~= nil)

    -- Generate base speech result
    local result = protocol:make(message) or { duration = 0 }

    -- Apply player voice suppression if configured
    local isPlayer = (message.sender == base.world.getPlayer())
    if base.vaicom.settings.playervoicedisabled and isPlayer then
        result.duration = 1.4
    else
        result.duration = result.duration + finalPause
    end

    -- Store result in current speech state
    base.vaicom.state.currentspeech = result

    -- Optional: force dummy sound playback (fallback / testing)
    if dummySound and not morze then
        result.files = { dummySound }
        result.directory = 'Sounds/Speech'
        result.duration = 1.0
    end

    return result
end
