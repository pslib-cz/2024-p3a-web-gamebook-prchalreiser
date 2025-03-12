import React, { useEffect, useRef } from "react";

interface RainbowSpiralProps {
  size?: number;
  speed?: number;
}

const RainbowSpiral: React.FC<RainbowSpiralProps> = ({
  size = window.innerWidth >= 1200 ? 400 : 250,
  speed = 0.02,
}) => {
  const canvasRef = useRef<HTMLCanvasElement>(null);

  useEffect(() => {
    const canvas = canvasRef.current;
    if (!canvas) return;

    const ctx = canvas.getContext("2d");
    if (!ctx) return;

    let animationFrameId: number;
    let rotation = 0;
    let time = 0;
    let pulseIntensity = 0.5; // Set default pulse intensity

    const draw = () => {
      time += 0.016;

      ctx.clearRect(0, 0, size, size);

      // Background gradient
      const gradient = ctx.createRadialGradient(
        size / 2,
        size / 2,
        0,
        size / 2,
        size / 2,
        size / 2
      );
      gradient.addColorStop(0, `rgba(0, 0, 0, ${0.2 + pulseIntensity * 0.2})`);
      gradient.addColorStop(1, "rgba(0, 0, 0, 0)");
      ctx.fillStyle = gradient;
      ctx.fillRect(0, 0, size, size);

      ctx.save();
      ctx.translate(size / 2, size / 2);

      // Base rotation speed
      rotation += speed + pulseIntensity * 0.1;
      ctx.rotate(rotation);

      // Calculate actual size based on hover state
      const currentSize = size;
      const maxRadius = currentSize * 0.45;

      const spiralPoints = 250;

      for (let i = 0; i < spiralPoints; i++) {
        const progress = i / spiralPoints;
        const angle = progress * Math.PI * 12;

        // Dynamic wave effect (now always active)
        const waveOffset =
          Math.sin(time * 3 + progress * Math.PI * 4) * (currentSize * 0.02) +
          pulseIntensity *
            currentSize *
            0.03 *
            Math.sin(progress * Math.PI * 8);

        const radius = progress * maxRadius + waveOffset;

        const x = Math.cos(angle) * radius;
        const y = Math.sin(angle) * radius;

        // Dynamic point size
        const baseSize = 4 + Math.sin(time * 4 + progress * Math.PI * 2) * 1.5;
        const pointSize = baseSize * (1 + pulseIntensity);

        // Dynamic colors
        const hue = progress * 360 + rotation * 30 + time * 20;
        const saturation = 90 + Math.sin(time + progress * Math.PI) * 10;
        const lightness = 60 + Math.sin(time * 2 + progress * Math.PI) * 10;

        // Glow effect
        ctx.shadowColor = `hsl(${hue}, ${saturation}%, ${lightness}%)`;
        ctx.shadowBlur = 15;
        ctx.fillStyle = `hsl(${hue}, ${saturation}%, ${lightness}%)`;

        ctx.beginPath();
        ctx.arc(x, y, pointSize, 0, Math.PI * 2);
        ctx.fill();

        // Connecting lines
        if (i > 0) {
          const prevAngle = ((i - 1) / spiralPoints) * Math.PI * 12;
          const prevWaveOffset =
            Math.sin(time * 3 + ((i - 1) / spiralPoints) * Math.PI * 4) *
            (currentSize * 0.02);
          const prevRadius =
            ((i - 1) / spiralPoints) * maxRadius + prevWaveOffset;
          const prevX = Math.cos(prevAngle) * prevRadius;
          const prevY = Math.sin(prevAngle) * prevRadius;

          ctx.beginPath();
          ctx.strokeStyle = `hsla(${hue}, ${saturation}%, ${lightness}%, 0.3)`;
          ctx.lineWidth = 2;
          ctx.moveTo(prevX, prevY);
          ctx.lineTo(x, y);
          ctx.stroke();
        }
      }

      ctx.restore();
      animationFrameId = requestAnimationFrame(draw);
    };

    draw();

    return () => {
      cancelAnimationFrame(animationFrameId);
    };
  }, [size, speed]);

  return (
    <canvas
      ref={canvasRef}
      width={size}
      height={size}
      style={{
        width: size,
        height: size,
        filter: `drop-shadow(0 0 20px rgba(255, 255, 255, 0.2))`,
        transform: "translateZ(0)",
        transition: "all 0.3s ease",
      }}
    />
  );
};
export default RainbowSpiral;
