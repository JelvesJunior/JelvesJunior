package com.example.streaming;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.streaming.models.Playlist;

import java.util.List;

public class PlaylistAdapter extends RecyclerView.Adapter<PlaylistAdapter.PlaylistViewHolder> {
    public interface OnPlaylistClickListener {
        void onPlaylistClick(Playlist playlist);
    }

    private List<Playlist> data;
    private final OnPlaylistClickListener listener;

    public PlaylistAdapter(List<Playlist> data, OnPlaylistClickListener listener) {
        this.data = data;
        this.listener = listener;
    }

    public void updateData(List<Playlist> newData) {
        this.data = newData;
        notifyDataSetChanged();
    }

    @NonNull
    @Override
    public PlaylistViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.item_playlist, parent, false);
        return new PlaylistViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull PlaylistViewHolder holder, int position) {
        Playlist playlist = data.get(position);
        holder.title.setText(playlist.getTitle());
        holder.description.setText(playlist.getDescription());
        holder.itemView.setOnClickListener(v -> listener.onPlaylistClick(playlist));
    }

    @Override
    public int getItemCount() {
        return data.size();
    }

    static class PlaylistViewHolder extends RecyclerView.ViewHolder {
        final TextView title;
        final TextView description;

        public PlaylistViewHolder(@NonNull View itemView) {
            super(itemView);
            title = itemView.findViewById(R.id.playlistTitle);
            description = itemView.findViewById(R.id.playlistDescription);
        }
    }
}
